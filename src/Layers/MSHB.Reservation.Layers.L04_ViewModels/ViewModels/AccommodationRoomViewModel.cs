using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class AccommodationRoomViewModel
    {
        public long AccommodationRoomId { get; set; }
        public string RoomNumber { get; set; }
        public long RoomPrice { get; set; }
        public int? BedRoom { get; set; }
        public RoomType RoomType { get; set; }
        public int Rank { get; set; }
        public int Bed { get; set; }
        public bool? IsEmpty { get; set; } = true;
        public bool? IsActivated { get; set; } = true;
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public long? CityId { get; set; }
    }
}
