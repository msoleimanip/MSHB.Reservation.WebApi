using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class UsersServiceErrors
    {
        public static readonly ReservationErrorMessage AddUserError =
           new ReservationErrorMessage("AUE-1000", "خطا در افزودن کاربر اتفاق افتاده است");
        public static readonly ReservationErrorMessage GroupNotFoundError =
           new ReservationErrorMessage("AUE-1001", "گروه ای که انتخاب کشهر اید وجود ندارد ");
        public static readonly ReservationErrorMessage CityNotFoundError =
           new ReservationErrorMessage("AUE-1002", "شهر ای که انتخاب کشهر اید وجود ندارد ");
        public static readonly ReservationErrorMessage UserExistError =
           new ReservationErrorMessage("AUE-1003", "کاربری با این نام در سیستم وجود دارد امکان اضافه کردن وجود ندارد.");
        public static readonly ReservationErrorMessage UserNotFoundError =
           new ReservationErrorMessage("AUE-1004", "کاربری با این مشخصات در سیستم وجود ندارد.");
        public static readonly ReservationErrorMessage AssignmentError =
           new ReservationErrorMessage("AUE-1005", "تخصیص به درستی صورت نپذیرفت.");
        public static readonly ReservationErrorMessage EquipmentNotFoundError =
           new ReservationErrorMessage("AUE-1006", "در تجهیزات انتخابی موردی است که یافت نشد.");
        public static readonly ReservationErrorMessage ChangeStateError =
           new ReservationErrorMessage("AUE-1007", "خطا در تغییر وضعیت کاربر");
        public static readonly ReservationErrorMessage ChangePasswordError =
           new ReservationErrorMessage("AUE-1008", "در تغییر پسورد خطایی رخ داده است.");


        









    }

}
