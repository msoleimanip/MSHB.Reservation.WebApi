using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("City_T")]
    public class City:BaseEntity
    {
        [DataType(DataType.Text), MaxLength(100)]
        public string CityName { get; set; }

        public string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public long? ParentId { get; set; }

        public bool? IsActivated { get; set; }
        public DateTime? DeactiveStartTime { get; set; }

        [ForeignKey("ParentId")]
        public virtual City Parent { get; set; }

        public virtual ICollection<City> Children { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<AccommodationRoom> AccommodationRooms { get; set; }
    }
}
