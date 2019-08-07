using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class CityServiceErrors
    {
        public static readonly ReservationErrorMessage GetError =
          new ReservationErrorMessage("CSE-1000", "هنگام درخواست کل شهر ها خطا اتفاق افتاد.");
        public static readonly ReservationErrorMessage NotFoundIdError =
          new ReservationErrorMessage("CSE-1001", "شناسه نامعتبر است و شهر در سامانه وجود ندارد.");
        public static readonly ReservationErrorMessage GetSingleError =
          new ReservationErrorMessage("CSE-1002", "هنگام درخواست شهر با شناسه مشخص خطا اتفاق افتاد.");
        public static readonly ReservationErrorMessage GetAllCitiesError =
         new ReservationErrorMessage("CSE-1003", "هنگام بارگذاری شهر ها خطا اتفاق افتاد.");
    }
}
