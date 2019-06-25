using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    public class AccommodationRoom:BaseEntity
    {
        [MaxLength(20)]
        public string RoomNumber { get; set; }
        public long RoomPrice { get; set; }
        public int? BedRoom { get; set; }
        public RoomType RoomType { get; set; }
        public int Rank { get; set; }
        public int Bed { get; set; }
        public bool? IsActivated { get; set; } = true;
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public string EvacuationTime { get; set; }
       
        public long? CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public virtual ICollection<AccommodationUserRoom> AccommodationUserRooms { get; set; }
       


    }
}
