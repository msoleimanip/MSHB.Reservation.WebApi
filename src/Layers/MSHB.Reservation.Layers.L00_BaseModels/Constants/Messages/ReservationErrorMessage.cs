namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages
{
    public class ReservationErrorMessage
    {
        public ReservationErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"کد پیام: {Code}\t متن پیام: {Message}";
        }

        public ReservationErrorMessage AddSqlErrorCode(int sqlErrorCode)
        {
            return new ReservationErrorMessage(Code + $" ({sqlErrorCode}) ", Message);
        }
    }
}