namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.RepositoryUow
{
    public class RepositoryUowErrors
    {
        public static readonly ReservationErrorMessage Commit =
            new ReservationErrorMessage("RUOW-1000", "هنگام commit تراکنش خطایی رخ داده است");
        public static readonly ReservationErrorMessage Rollback =
            new ReservationErrorMessage("RUOW-1001", "هنگام Rollback تراکنش خطایی رخ داده است");
        public static readonly ReservationErrorMessage Dispose =
            new ReservationErrorMessage("RUOW-1002", "هنگام dispose کردن UOW خطایی رخ داده است");
    }
}
