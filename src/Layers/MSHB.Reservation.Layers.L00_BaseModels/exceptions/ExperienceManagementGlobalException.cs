using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages;


namespace MSHB.Reservation.Layers.L00_BaseModels.exceptions
{
    public class ReservationGlobalException : Exception
    {
        public ReservationGlobalException(ReservationErrorMessage error)
        {
            UserMessage = error.Message;
            ErrorCode = error.Code;
            ExceptionList = null;
        }

        public ReservationGlobalException(ReservationErrorMessage error, Exception e)
        {
            UserMessage = error.Message;
            ErrorCode = error.Code;
            ExceptionList = new List<Exception>{e};
        }
        public ReservationGlobalException(ReservationErrorMessage error, params Exception[] exceptions)
        {
            UserMessage = error.Message;
            ErrorCode = error.Code;
            ExceptionList = new List<Exception>();
            ExceptionList.AddRange(exceptions);
        }

    

        public string UserMessage { get; set; }
        public string ErrorCode { get; set; }
        public List<Exception> ExceptionList { get; set; }
    }
}