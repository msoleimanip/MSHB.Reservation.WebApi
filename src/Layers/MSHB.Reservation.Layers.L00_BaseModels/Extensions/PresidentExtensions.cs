
using MSHB.Reservation.Layers.L01_Entities.Enums;
using MSHB.Reservation.Layers.L01_Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Extensions
{
    public static class PresidentExtensions
    {
        public static bool IsAdmin(this User user)
        {
            return user.IsPresident != null && user.IsPresident == PresidentType.Admin ? true : false;
        }
    }
}
