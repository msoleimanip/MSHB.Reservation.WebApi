using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class GroupServiceErrors
    {
        public static readonly ReservationErrorMessage GetRolesError =
           new ReservationErrorMessage("GRE-1000", "خطا در دریافت Role");
        public static readonly ReservationErrorMessage AddGroupError =
           new ReservationErrorMessage("GRE-1001", "مشکل در ساخت گروه جدید");
        public static readonly ReservationErrorMessage SameGroupExistError =
          new ReservationErrorMessage("GRE-1002", "گروه با نام مشابه وجود دارد");
        public static readonly ReservationErrorMessage DeleteGroupError =
          new ReservationErrorMessage("GRE-1003", "خطا در حذف گروه درخواست شده");
        public static readonly ReservationErrorMessage EditGroupError =
          new ReservationErrorMessage("GRE-1004", "خطا در ویرایش گروه درخواست شده");
        public static readonly ReservationErrorMessage EditDuplicateGroupError =
          new ReservationErrorMessage("GRE-1005", "امکان تغییر به Group با نام تکراری مهیا نیست.");
        public static readonly ReservationErrorMessage UserInGroupExistError =
          new ReservationErrorMessage("GRE-1006", "گروه انتخابی دارای کاربر می باشد ابتدا آن کاربر را به گروه دیگر تخصیص دهید.");
        public static readonly ReservationErrorMessage DeleteGroupNotselectedError =
          new ReservationErrorMessage("GRE-1007", "درخواست شما منجر به حذف Group می شود که در لیست انتخابی نیست.");
        public static readonly ReservationErrorMessage GetGroupError =
          new ReservationErrorMessage("GRE-1008", "در گرفتن Group مورد نظر خطایی رخ داده است.");
        public static readonly ReservationErrorMessage RoleExistError =
          new ReservationErrorMessage("GRE-1009", "بخشی یا کل رول ها در لیست انتخابی وجود ندارد");
        public static readonly ReservationErrorMessage EditGroupNotExistError =
          new ReservationErrorMessage("GRE-1010", "گروهی که می خواهید  تغییر دهید وجود ندارد");
        public static readonly ReservationErrorMessage GetGroupAuthenticationByIdError =
          new ReservationErrorMessage("GRE-1011", "گروه مربوط به شناسه ارسالی در سامانه وجود ندارد");
    }

}
