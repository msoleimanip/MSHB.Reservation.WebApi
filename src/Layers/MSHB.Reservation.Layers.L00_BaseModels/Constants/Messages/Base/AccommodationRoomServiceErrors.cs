using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class AccommodationRoomServiceErrors
    {
        public static readonly ReservationErrorMessage AddAccommodationError =
          new ReservationErrorMessage("AAE-1000", "هنگام اضافه کردن مکان به اقامتگاه خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage AddDuplicateAccommodationError =
        new ReservationErrorMessage("AAE-1001", "اضافه کردن مکان تکراری به اقامتگاه  ممکن نیست");
        public static readonly ReservationErrorMessage EditDuplicateAccommodationError =
          new ReservationErrorMessage("AAE-1002", "تغییر در  مکان  اقامتگاه منجر به مکان  اقامتگاه تکراری می شود");
        public static readonly ReservationErrorMessage EditAccommodationNotExistError =
          new ReservationErrorMessage("AAE-1003", "مکان  اقامتگاه ی که به دنبال تغییر هستید وجود ندارد");
        public static readonly ReservationErrorMessage GetAccommodationNotExistError =
          new ReservationErrorMessage("AAE-1009", "مکان  اقامتگاه ی که به دنبال دریافت هستید وجود ندارد");
        public static readonly ReservationErrorMessage EditAccommodationError =
        new ReservationErrorMessage("AAE-1004", "هنگام تغییر دادن مکان  اقامتگاه خطایی رخ داده  است.");
        public static ReservationErrorMessage ChangeStatusError =
        new ReservationErrorMessage("AAE-1005", "در تغییر وضعیت  اتاق اقامتگاه خطایی رخ داده است");

        public static readonly ReservationErrorMessage DeleteAccommodationError =
          new ReservationErrorMessage("AAE-1006", "هنگام حذف مکانی ازاقامتگاه خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage UserInAccommodationExistError =
         new ReservationErrorMessage("AAE-1007", "در مکانی از  اقامتگاه که خواهان حذف هستید اطلاعات کاربر وجود دارد");

        public static readonly ReservationErrorMessage GetAccommodationError =
         new ReservationErrorMessage("AAE-1008", "دریافت اطلاعات از اتاق ها خطا رخ داده است");


        
    }
}
