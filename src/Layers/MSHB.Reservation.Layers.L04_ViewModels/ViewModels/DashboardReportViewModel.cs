using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class DashboardReportViewModel
    {
        public long AccommodationCount { get; set; } = 0;
        public long AccommodationUserCount { get; set; } = 0;
        public long ReservationCount { get; set; } = 0;
        public long UserCount { get; set; } = 0;
    }
}
