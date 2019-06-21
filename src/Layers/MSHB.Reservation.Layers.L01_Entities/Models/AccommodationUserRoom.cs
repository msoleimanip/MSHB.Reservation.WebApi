using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    public class AccommodationUserRoom:BaseEntity
    {       
        [Required]
        
        public long AccommodationRoomId { get; set; }
        [ForeignKey("AccommodationRoomId")]
        public virtual AccommodationRoom AccommodationRoom { get; set; }

        public long CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? EndTime { get; set; }
        [MaxLength(12)]
        public string NationalCode { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public GenderType GenderType { get; set; }
        public string PersonalCode { get; set; }
        
        public long SystemCode { get; set; }
        public int GuestCounts { get; set; }
        public long Description { get; set; }
        public long PriceAccommodation { get; set; }
        public PaymentType PaymentType { get; set; }             
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<AccommodationUserAttachment> AccommodationUserAttachments { get; set; }


        

    }
}
