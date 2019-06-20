using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class ReservationUserRoomServiceErrors
    {
        public static readonly ReservationErrorMessage AddReservationError =
          new ReservationErrorMessage("RUR-1000", "هنگام اضافه کردن رزرو به اقامتگاه خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage AddDuplicateReservationError =
        new ReservationErrorMessage("RUR-1001", "اضافه کردن رزرو تکراری به اقامتگاه  ممکن نیست");
        public static readonly ReservationErrorMessage EditDuplicateReservationError =
          new ReservationErrorMessage("RUR-1002", "تغییر در  رزرو  اقامتگاه منجر به مکان  اقامتگاه تکراری می شود");
        public static readonly ReservationErrorMessage EditReservationNotExistError =
          new ReservationErrorMessage("RUR-1003", "رزرو  اقامتگاه ی که به دنبال تغییر هستید وجود ندارد");
        public static readonly ReservationErrorMessage EditReservationError =
            new ReservationErrorMessage("RUR-1004", "هنگام تغییر رزرو  اقامتگاه خطایی رخ داده  است.");
     
        public static readonly ReservationErrorMessage DeleteReservationError =
          new ReservationErrorMessage("RUR-1006", "هنگام حذف رزرو خطایی رخ داده  است.");

        public static readonly ReservationErrorMessage GetReservationError =
         new ReservationErrorMessage("RUR-1008", "دریافت اطلاعات رزرو خطا رخ داده است");


        
    }
}
