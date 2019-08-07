using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("Accommodation_T")]
    public class Accommodation : BaseEntity
    {

        public Accommodation()
        {

        }

        public string Caption { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public string Code { get; set; }

        [DefaultValue("false")]
        public bool IsActivated { get; set; }

        public string District { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }


        public Guid? FileId { get; set; }

        public long CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<AccommodationAttachment> AccommodationAttachments { get; set; }
    }
}
