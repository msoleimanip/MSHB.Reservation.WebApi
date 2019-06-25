using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class UploadServiceErrors
    {
        public static readonly ReservationErrorMessage UploadFileError =
        new ReservationErrorMessage("UFE-1000", "هنگام آپلود فایل خطایی رخ داده است.");
        public static readonly ReservationErrorMessage UploadFileValidError =
          new ReservationErrorMessage("UFE-1001", "فایل ارسالی بایستی معتبر باشد.");
    }
}
