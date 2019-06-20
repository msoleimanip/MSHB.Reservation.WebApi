using System;

namespace MSHB.Reservation.Shared.Common.Captcha.Providers
{
    /// <summary>
    /// Runtime Guards
    /// </summary>
    internal static class Guards
    {
        /// <summary>
        /// Checks if the argument is null.
        /// </summary>
        public static void CheckArgumentNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks if the parameter is null.
        /// </summary>
        public static void CheckMandatoryOption(this string s, string name)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException(name);
        }
    }
}