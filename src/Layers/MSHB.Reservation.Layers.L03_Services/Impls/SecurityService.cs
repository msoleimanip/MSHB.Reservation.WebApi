using MSHB.Reservation.Layers.L03_Services.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class SecurityService : ISecurityService
    {
        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}