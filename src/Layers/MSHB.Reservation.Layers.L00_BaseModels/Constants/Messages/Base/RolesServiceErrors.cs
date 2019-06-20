using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class RolesServiceErrors
    {
        public static readonly ReservationErrorMessage GetRolesError =
           new ReservationErrorMessage("GRE-1000", "خطا در دریافت Role");
        public static readonly ReservationErrorMessage FindUserRolesError =
          new ReservationErrorMessage("GRE-1001", "خطا در پیدا کردن Role");
        public static readonly ReservationErrorMessage GetIsUserInRoleError =
          new ReservationErrorMessage("GRE-1002", "خطا در تشخیص رول مشخصی از کاربر");
        public static readonly ReservationErrorMessage GetFindUsersInRoleError =
          new ReservationErrorMessage("GRE-1003", "خطا در پیدا کردن کاربران رول مشخص");

        
            
            

    }
    
}
