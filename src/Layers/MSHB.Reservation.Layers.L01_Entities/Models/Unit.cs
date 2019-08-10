using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("Unit_T")]
    public class Unit : BaseEntity
    {
        public AccommodationType AccommodationType { get; set; }
        public int RoomCount { get; set; }
        public int SingleBedCount { get; set; }
        public int DoubleBedCount { get; set; }
        public int MinimumCount { get; set; }
        public int MaximumCount { get; set; }
        public bool IsActive { get; set; }
        public long AccommodationId { get; set; }
        public virtual Accommodation Accommodation { get; set; }
    }
}
