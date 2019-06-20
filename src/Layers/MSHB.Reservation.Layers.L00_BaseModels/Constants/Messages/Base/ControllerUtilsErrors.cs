using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class ControllerUtilsErrors
    {
        public static readonly ReservationErrorMessage UserRolesNotFound =
           new ReservationErrorMessage("URN-1000", "Role کاربر یافت نشده است");
        public static readonly ReservationErrorMessage GetUserRoles =
          new ReservationErrorMessage("GUR-1000", "در گرفتن Role کاربر خطایی رخ داده است.");
        public static readonly ReservationErrorMessage GetUserError =
          new ReservationErrorMessage("GUE-1000", "در بدست آوردن کاربر خطایی رخ داده است");
        public static readonly ReservationErrorMessage GetUserPresidentError =
         new ReservationErrorMessage("GUP-1000", "در بدست آوردن سطح کاربر خطایی رخ داده است");
    }
}
