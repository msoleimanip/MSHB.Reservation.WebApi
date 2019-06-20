namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class BaseRepositoryErrors
    {
        public static readonly ReservationErrorMessage InternalCommit =
            new ReservationErrorMessage("RBA-1000", "هنگام commit تراکنش داخلی خطایی رخ داده است");
        public static readonly ReservationErrorMessage InternalRollback =
            new ReservationErrorMessage("RBA-1001", "هنگام Rollback تراکنش داخلی خطایی رخ داده است");
    }
}
