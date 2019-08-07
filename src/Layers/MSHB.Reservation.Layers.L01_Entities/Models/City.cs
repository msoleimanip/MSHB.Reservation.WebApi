using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("City_T")]
    public class City : BaseEntity
    {
        public string Title { get; set; }

        public long ProvinceId { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Accommodation> Accommodations { get; set; }
    }
}
