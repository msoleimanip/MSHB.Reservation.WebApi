using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class ProvinceServiceErrors
    {
        public static readonly ReservationErrorMessage GetError =
         new ReservationErrorMessage("PSE-1000", "خطا در هنگام درخواست کل استان ها");
        public static readonly ReservationErrorMessage NotFoundIdError =
       new ReservationErrorMessage("PSE-1001", "شناسه استان نامعتبر است");
        public static readonly ReservationErrorMessage GetSingleError =
       new ReservationErrorMessage("PSE-1002", "خطا در هنگام درخواست استان");
    }
}
