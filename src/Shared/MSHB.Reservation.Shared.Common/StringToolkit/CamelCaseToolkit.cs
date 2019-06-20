namespace MSHB.Reservation.Shared.Common.StringToolkit
{
    public class CamelCaseToolkit
    {
        public static string ToCamelCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";
            if (input.Length == 1)
                return input[0].ToString().ToLower();

            return input[0].ToString().ToLower() + input.Substring(1, input.Length - 1);
        }
    }
}
