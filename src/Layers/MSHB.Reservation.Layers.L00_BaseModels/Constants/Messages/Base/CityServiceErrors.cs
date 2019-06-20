using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class CityServiceErrors
    {
        public static readonly ReservationErrorMessage AddCityError =
          new ReservationErrorMessage("AOE-1000", "هنگام اضافه کردن شهر خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage EditCityError =
          new ReservationErrorMessage("AOE-1001", "هنگام تغییر دادن شهر خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage DeleteCityError =
          new ReservationErrorMessage("DEO-1002", "هنگام حذف شهر خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage CityNotFoundError =
          new ReservationErrorMessage("ONE-1003", "شهر مورد نظر یافت نشده است");
        public static readonly ReservationErrorMessage AddDuplicateCityError =
          new ReservationErrorMessage("ONE-1004", "اضافه کردن شهر تکراری ممکن نیست");
        public static readonly ReservationErrorMessage EditDuplicateCityError =
          new ReservationErrorMessage("ONE-1005", "تغییر در شهر منجر به شهر تکراری می شود");
        public static readonly ReservationErrorMessage EditCityNotExistError =
          new ReservationErrorMessage("ONE-1006", "شهر ای که به دنبال تغییر هستید وجود ندارد");
        public static readonly ReservationErrorMessage UserInCityExistError =
          new ReservationErrorMessage("ONE-1007", "در شهر و سلسله مراتب آن کاربر وجود دارد");
        public static readonly ReservationErrorMessage DeleteCityNotselectedError =
          new ReservationErrorMessage("ONE-1008", "شهر که برای حذف انتخاب شده منجر به حذف شهر های انتخاب نشده می شود لطفا همه موارد انتخاب شود.");
        public static readonly ReservationErrorMessage GetCityError =
          new ReservationErrorMessage("ONE-1009", "در دریافت اطلاعات شهر خطا رخ داده است");


    }
}
