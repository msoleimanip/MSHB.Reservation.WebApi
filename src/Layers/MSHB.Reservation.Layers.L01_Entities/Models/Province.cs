using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("Province_T")]
    public class Province : BaseEntity
    {
        public string Title { get; set; }       

        public virtual ICollection<City> Cities { get; set; }
    }
}
