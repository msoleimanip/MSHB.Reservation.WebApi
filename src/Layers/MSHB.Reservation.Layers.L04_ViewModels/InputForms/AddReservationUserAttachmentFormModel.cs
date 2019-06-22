using MSHB.Reservation.Layers.L01_Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AddReservationUserAttachmentFormModel
    {
        [Required(ErrorMessage = "باید برای همراه جدید، اتاق انتخاب کنید")]
        public long? AccommodationUserRoomId { get; set; }
        public GenderType GenderType { get; set; }
        [Required(ErrorMessage = "باید برای همراه جدید، نام انتخاب کنید"), MinLength(1)]
        public string Name { get; set; }
        
        [MaxLength(20)]
        public string NationalCode { get; set; }
        public string Relative { get; set; }
        public int? Age { get; set; }
    }
}