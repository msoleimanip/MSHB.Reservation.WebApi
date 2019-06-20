using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class GroupRepositoryErrors
    {
        public static readonly ReservationErrorMessage DbAddGroupError =
          new ReservationErrorMessage("DAGE-1000", "هنگام اضافه کردن Group خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage DbEditGroupError =
          new ReservationErrorMessage("DAGE-1001", "هنگام تغییر دادن Group خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage DbDeleteGroupError =
          new ReservationErrorMessage("DAGE-1002", "هنگام حذف Group خطایی رخ داده  است.");

        public static readonly ReservationErrorMessage DbEditDuplicateGroupError =
          new ReservationErrorMessage("DAGE-1003", "امکان تغییر به Group با نام تکراری مهیا نیست.");
        public static readonly ReservationErrorMessage UserInGroupExistError =
          new ReservationErrorMessage("DAGE-1004", "گروه انتخابی دارای کاربر می باشد ابتدا آن کاربر را به گروه دیگر تخصیص دهید.");
        public static readonly ReservationErrorMessage DeleteGroupNotselectedError =
          new ReservationErrorMessage("DAGE-1005", "درخواست شما منجر به حذف Group می شود که در لیست انتخابی نیست.");      
        public static readonly ReservationErrorMessage DbGetGroupError =
          new ReservationErrorMessage("DAGE-1007", "در گرفتن Group مورد نظر خطایی رخ داده است.");
        public static readonly ReservationErrorMessage DbRoleExistError =
          new ReservationErrorMessage("DAGE-1007", "بخشی یا کل رول ها در لیست انتخابی وجود ندارد");
        public static readonly ReservationErrorMessage DbEditGroupNotExistError =
          new ReservationErrorMessage("DAGE-1006", "گروهی که می خواهید  تغییر دهید وجود ندارد");
        public static readonly ReservationErrorMessage DbGetRolesError =
          new ReservationErrorMessage("DAGE-1008", "در دریافت Role ها خطایی رخ داده است.");




    }
}
