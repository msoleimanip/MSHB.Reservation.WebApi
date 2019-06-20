using Reservation.WebUI.Layers.L00_BaseModels.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace MSHB.Reservation.Layers.L03.Services.Logger
{
    public static class GlobalExceptionHandler
    {
        private static ILoggerFactory _loggerFactory;

        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {

                    context.Response.ContentType = "application/json";
                    var ex = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                    if (ex != null)
                        switch (ex.Error.GetType().Name)
                        {
                            case "ReservationGlobalException":
                                ReservationGlobalExceptionHandler(ex, context);
                                break;
                            case "System.AccessViolationException":
                                break;
                            default:
                                DefaultExceptionHandler(ex, context);
                                break;
                        }
                });
            });
        }


        private static async void ReservationGlobalExceptionHandler(IExceptionHandlerFeature ex, HttpContext context)
        {
            ReservationGlobalException exception = (ReservationGlobalException)ex.Error;
            var logger = _loggerFactory.CreateLogger($"{exception.ErrorCode} - GlobalExceptionHandler");
            logger.LogError(context.User.Identity.Name, GetLogMessage(new List<Exception> { exception }));
            List<ReservationErrorMessage> detailErrorList = GetExceptionErrors(exception.ExceptionList);

            RequestResultViewModel result = new RequestResultViewModel
            {
                Data = null,
                ErrorCode = exception.ErrorCode,
                ErrorMessage = exception.UserMessage,
                DetailErrorList = detailErrorList

            };
            var serializerSetting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResult = JsonConvert.SerializeObject(result, serializerSetting);
            context.Response.StatusCode = 501;
            await context.Response.WriteAsync(jsonResult);
        }

        private static async void DefaultExceptionHandler(IExceptionHandlerFeature ex, HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var err = JsonConvert.SerializeObject(new
            {
                ex.Error.StackTrace,
                ex.Error.Message
            });
            await
                context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length)
                    .ConfigureAwait(false);
        }


        private static List<ReservationErrorMessage> GetExceptionErrors(List<Exception> exceptions)
        {

            List<ReservationErrorMessage> detailErrorList = new List<ReservationErrorMessage>();
            if (exceptions != null && exceptions.Count > 0)
            {
                foreach (Exception ex in exceptions)
                {
                    if (ex is ReservationGlobalException exception)
                    {
                        detailErrorList.Add(new ReservationErrorMessage(exception.ErrorCode, exception.UserMessage));

                        detailErrorList.AddRange(GetExceptionErrors(exception.ExceptionList));

                    }
                    else
                    {
                        detailErrorList.Add(new ReservationErrorMessage("EXC-500", ex.Message));
                    }
                }


            }
            return detailErrorList;
        }

        private static string GetLogMessage(List<Exception> exceptions)
        {

            string logMessage = "";
            if (exceptions != null && exceptions.Count > 0)
            {

                foreach (Exception ex in exceptions)
                {
                    if (ex is ReservationGlobalException exception)
                    {
                        logMessage = logMessage + exception.ErrorCode + " - " + exception.UserMessage + Environment.NewLine;
                        logMessage = logMessage + exception;
                        logMessage = logMessage + GetLogMessage(exception.ExceptionList);

                    }
                    else
                    {

                        logMessage = logMessage + ex + Environment.NewLine;
                    }
                }

            }
            return logMessage;
        }
    }

}
