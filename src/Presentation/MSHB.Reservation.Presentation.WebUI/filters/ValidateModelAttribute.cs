using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages;
using Reservation.WebUI.Layers.L00_BaseModels.Settings;

namespace MSHB.Reservation.Presentation.WebUI.filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private readonly string _errorCode;

        public ValidateModelAttribute(string errorCode = "")
        {
            _errorCode = errorCode;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.Parameters.Count > 0) // has a parameter 
            {
                var isParamOptional = ((ControllerParameterDescriptor) context.ActionDescriptor.Parameters[0])
                    .ParameterInfo.IsOptional;
                if (!isParamOptional) // parameter is not optional
                {
                    if (IsParamNullOrEmpty(context)) // parameter is null
                    {
                        var errorMessage = GetParameterErrors(context);
                        context.Result =
                            new BadRequestObjectResult(GetRequestResult(context, errorMessage, _errorCode));
                    }
                    else if (!context.ModelState.IsValid)
                    {
                        context.Result= new BadRequestObjectResult(GetRequestResult(context.ModelState,
                            _errorCode));
                    }
                }
            }


            base.OnActionExecuting(context);
        }

        private static bool IsParamNullOrEmpty(ActionExecutingContext context)
        {
            return context.ActionArguments == null || context.ActionArguments.Count == 0 || context.ActionArguments.Any(
                       arg =>
                       {
                           bool result = false;
                           if (arg.Value is string) result = string.IsNullOrWhiteSpace((string) arg.Value);
                           else if (arg.Value is List<object>)
                               result = arg.Value == null || !((List<object>) arg.Value).Any();
                           else if (arg.Value is List<Guid>)
                               result = arg.Value == null || !((List<Guid>) arg.Value).Any();


                           return result;
                       });
        }

        private RequestResultViewModel GetRequestResult(ActionExecutingContext context, string errorMessage,
            string errorCode)
        {
            if (errorMessage == null)
            {
                var dn =
                    ((ControllerActionDescriptor) context.ActionDescriptor)
                    .MethodInfo
                    .GetCustomAttributes(typeof(DisplayNameAttribute), false);
                var displayName =
                    ((ControllerActionDescriptor) context.ActionDescriptor)
                    .ActionName;
                if (dn.Any())
                {
                    displayName =
                        ((DisplayNameAttribute[]) dn)[0].DisplayName;
                }

                errorMessage = $"ورودی عملیات \"{displayName}\" نا معتبر است";
            }
            return GetRequestResult(errorMessage, errorCode);
        }

        private RequestResultViewModel GetRequestResult(string message, string errorCode)
        {
            RequestResultViewModel res = new RequestResultViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = message,
            };
            return res;
        }

        private RequestResultViewModel GetRequestResult(ModelStateDictionary modelState, string errorCode )
        {
            RequestResultViewModel res = new RequestResultViewModel
            {
                ErrorMessage = "اطلاعات ورودی نامعتبر است",

                ErrorCode =  errorCode,
                DetailErrorList = GetModelStateDetailErrors(modelState, errorCode),
                ValidationMessages = GetModelStateErrors(modelState)
            };
            if (modelState.Values.Any())
            {
                var error = modelState.Values.SelectMany(c => c.Errors).Select(c=>c.ErrorMessage).ToList();
                if (error.Count>0)
                res.ErrorMessage = string.Join(", ", error);
            }
            return res;
        }

        private List<ReservationErrorMessage> GetModelStateDetailErrors(ModelStateDictionary modelState, string errorCode)
        {
            List<ReservationErrorMessage> modelErrors = new List<ReservationErrorMessage>();
            if (modelState.Keys.Any())
            {
                var keys = modelState.Keys.Where(x => x != "").ToList();
                foreach (var stateKey in keys)
                {
                    foreach (var error in modelState[stateKey].Errors)
                    {
                        modelErrors.Add(new ReservationErrorMessage(errorCode, error.ErrorMessage));
                    }
                }
            }
            return modelErrors;
        }

        private Dictionary<string, string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            Dictionary<string, string> errorsDic = new Dictionary<string, string>();
            if (modelState.Keys.Any())
            {
                var keys = modelState.Keys.Where(x => x != "").ToList();
                foreach (var stateKey in keys)
                {
                    foreach (var error in modelState[stateKey].Errors)
                    {
                        if (!errorsDic.ContainsKey(stateKey))
                            errorsDic.Add(stateKey, error.ErrorMessage);
                        else
                            errorsDic[stateKey] = errorsDic[stateKey] + "\t" + error.ErrorMessage;
                    }
                }
            }
            return errorsDic;
        }

        private string GetParameterErrors(ActionExecutingContext context)
        {
            string errorMessage = "";
            var attribute = ((ControllerParameterDescriptor) context.ActionDescriptor.Parameters[0])
                .ParameterInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(RequiredAttribute));
            var paramErrorMessage = attribute?.NamedArguments?.FirstOrDefault(x => x.MemberName == "ErrorMessage")
                .TypedValue.Value?.ToString();
            if (!string.IsNullOrEmpty(paramErrorMessage))
            {
                errorMessage = errorMessage + Environment.NewLine + paramErrorMessage;
            }


            return errorMessage;
        }
    }
}