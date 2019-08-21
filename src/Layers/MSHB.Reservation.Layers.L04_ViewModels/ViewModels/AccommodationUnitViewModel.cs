using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class AccommodationUnitViewModel
    {
        public long UnitId { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int RoomCount { get; set; }
        public int SingleBedCount { get; set; }
        public int DoubleBedCount { get; set; }
        public int MinimumCount { get; set; }
        public int MaximumCount { get; set; }
        public bool IsActive { get; set; }
    }
}
