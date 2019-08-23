using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public static class BookinationServiceErrors
    {
        public static readonly ReservationErrorMessage AddError =
          new ReservationErrorMessage("BAE-1000", "هنگام ثبت رزرو خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage UnitNotFoundError =
        new ReservationErrorMessage("BAE-1001", "واحد مورد نظر یافت نشد");
        public static readonly ReservationErrorMessage GetError =
        new ReservationErrorMessage("BAE-1002", "هنگام خواندن داده ها خطایی رخ داد");
        public static readonly ReservationErrorMessage AddEntourageError =
        new ReservationErrorMessage("BAE-1003", "هنگام ذخیره اطلاعات خطایی رخ داد");
    }
}
