using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("AccommodationUserAttachment_T")]
    public class AccommodationUserAttachment:BaseEntity
    {
        [Required]
        
        public long AccommodationUserRoomId { get; set; }
        [ForeignKey("AccommodationUserRoomId")]
        public virtual AccommodationUserRoom AccommodationUserRoom { get; set; }
        public GenderType GenderType { get; set; }
        public string Name { get; set; }
        [MaxLength(20)]
        public string NationalCode { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Relative { get; set; }
        public int? Age { get; set; }


    }
}
