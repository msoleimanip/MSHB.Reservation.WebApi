using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    public class AccommodationRoom:BaseEntity
    {
        public string RoomNumber { get; set; }
        public string RoomPrice { get; set; }
        public int? BedRoom { get; set; }
        public RoomType RoomType { get; set; }
        public int Rank { get; set; }
        public int Bed { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }

    }
}
