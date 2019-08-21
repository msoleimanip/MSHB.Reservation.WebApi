using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public static class BookinationServiceErrors
    {
        public static readonly ReservationErrorMessage AddError =
          new ReservationErrorMessage("RAE-1000", "هنگام ثبت رزرو خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage UnitNotFoundError =
        new ReservationErrorMessage("RAE-1001", "واحد مورد نظر یافت نشد");
    }
}
