namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class AccommodationUserAttachmentServiceErrors
    {
     
        public static readonly ReservationErrorMessage AddAccommodationUserAttachmentError =
          new ReservationErrorMessage("AAUM-1000", "هنگام اضافه کردن همراهان خطایی رخ داده  است.");
        public static readonly ReservationErrorMessage AddDuplicateAccommodationUserAttachmentError =
        new ReservationErrorMessage("AAUM-1001", "اضافه کردن همراه تکراری به اقامتگاه  ممکن نیست");
        public static readonly ReservationErrorMessage EditDuplicateAccommodationUserAttachmentError =
          new ReservationErrorMessage("AAUM-1002", "تغییر در  همراهان منجر به همراهان تکراری می شود");
        public static readonly ReservationErrorMessage EditAccommodationUserAttachmentNotExistError =
          new ReservationErrorMessage("AAUM-1003", "همراهان که به دنبال تغییر هستید وجود ندارد");
        public static readonly ReservationErrorMessage EditAccommodationUserAttachmentError =
        new ReservationErrorMessage("AAUM-1004", "هنگام تغییر دادن همراهان  اقامتگاه خطایی رخ داده  است.");
        

        public static readonly ReservationErrorMessage DeleteAccommodationUserAttachmentError =
          new ReservationErrorMessage("AAUM-1006", "هنگام حذف همراهان خطایی رخ داده  است.");
      
        public static readonly ReservationErrorMessage GetAccommodationUserAttachmentError =
         new ReservationErrorMessage("AAUM-1008", "دریافت اطلاعات از همراهان خطا رخ داده است");
    }
}