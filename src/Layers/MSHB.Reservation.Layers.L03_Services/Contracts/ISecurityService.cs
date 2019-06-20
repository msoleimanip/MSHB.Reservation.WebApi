using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface ISecurityService
    {
        string GetSha256Hash(string input);
    }
}
