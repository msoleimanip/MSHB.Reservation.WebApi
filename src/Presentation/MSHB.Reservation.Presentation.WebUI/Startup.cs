using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DNTCommon.Web.Core;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MSHB.Reservation.Layers.L00_BaseModels.Security;
using MSHB.Reservation.Layers.L00_BaseModels.Settings;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03.Services.Logger;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L03_Services.Impls;
using MSHB.Reservation.Layers.L03_Services.Initialization;
using MSHB.Reservation.Presentation.WebUI.Utils;
using MSHB.Reservation.Shared.Common.PersianToolkit;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace MSHB.Reservation.Presentation.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration { set; get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(provider => Configuration);
            services.Configure<SiteSettings>(options => Configuration.Bind(options));
            services.Configure<RepositorySettings>(options => Configuration.Bind(options));
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                //options.Providers.Add<CustomCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml", "application/xml" });
            });

            services.Configure<BearerTokensOptions>(options => Configuration.GetSection("BearerTokens").Bind(options));
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IDbInitializerService, DbInitializerService>();
            services.AddTransient<ITokenStoreService, TokenStoreService>();
            services.AddTransient<ITokenValidatorService, TokenValidatorService>();
            services.AddTransient<IGroupAuthenticationService, GroupAuthenticationService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IAccommodationService, AccommodationService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddSingleton(new HttpClient());


            services.AddMemoryCache();



            services.AddDbContext<ReservationDbContext>(options =>
            {
                options.UseLazyLoadingProxies(true).UseSqlServer(
                    Configuration.GetConnectionString("SqlServer:ApplicationDbContextConnection"),
                    serverDbContextOptionsBuilder =>
                    {
                        var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                        serverDbContextOptionsBuilder.CommandTimeout(minutes);
                        serverDbContextOptionsBuilder.EnableRetryOnFailure();
                    });

            });


            services.AddOData();
            try
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("V1", new Info { Title = "MSHB.Reservation API", Version = "V1" });
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            // Needed for jwt auth.
            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["BearerTokens:Issuer"], // site that makes the token
                        ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                        ValidAudience = Configuration["BearerTokens:Audience"], // site that consumes the token
                        ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["BearerTokens:Key"])),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("Authentication failed.", context.Exception);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                            return tokenValidatorService.ValidateAsync(context);
                        },
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                            return Task.CompletedTask;
                        }
                    };
                });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:4200") //Note:  The URL must be specified without a trailing slash (/)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()

                      );
            });

            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");

            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                options.UseYeKeModelBinder();
                options.UseCustomStringModelBinder();


                // options.Filters.Add(new NoBrowserCacheAttribute());
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddResponseCompression();
            services.AddDNTCommonWeb();
            services.AddRazorViewRenderer();
            services.AddMvcActionsDiscoveryService();
            services.AddProtectionProviderService();
            services.AddCloudscribePagination();
        }

        public void Configure(ILoggerFactory loggerFactory, IApplicationBuilder app, IHostingEnvironment env)
        {
            try
            {

                loggerFactory.AddLog4Net();
                loggerFactory.AddDbLogger(serviceProvider: app.ApplicationServices, scopeFactory: app.ApplicationServices.GetRequiredService<IServiceScopeFactory>(), minLevel: LogLevel.Warning);
                app.UseGlobalExceptionHandler(loggerFactory);

                app.UseAngularAntifCityeryToken();

                app.UseAuthentication();




                app.UseFileServer();

                app.UseResponseCompression();


                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/V1/swagger.json", "MSHB.Reservation API V1");

                });

                app.UseStatusCodePages();
                app.UseDefaultFiles(); // so index.html is not required
                app.UseStaticFiles();

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");


                });
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw;
            }
        }
    }
}