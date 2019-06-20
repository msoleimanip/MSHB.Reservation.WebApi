using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MSHB.Reservation.Presentation.WebUI.Utils
{
    public class AngularAntifCityeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antifCityery;

        public AngularAntifCityeryTokenMiddleware(RequestDelegate next, IAntiforgery antifCityery)
        {
            _next = next;
            _antifCityery = antifCityery;
        }

        public Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;
            if (path != null && !path.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
            {
                var tokens = _antifCityery.GetAndStoreTokens(context);
                context.Response.Cookies.Append(
                      key: "XSRF-TOKEN",
                      value: tokens.RequestToken,
                      options: new CookieOptions
                      {
                          HttpOnly = false // Now JavaScript is able to read the cookie
                      });
            }
            return _next(context);
        }
    }

    public static class AntifCityeryTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAngularAntifCityeryToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AngularAntifCityeryTokenMiddleware>();
        }
    }
}