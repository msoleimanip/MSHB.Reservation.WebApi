using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class ReservationUserAttachmentViewModel
    {
        
        public long Id { get; set; }
        public long AccommodationUserRoomId { get; set; }
        public GenderType GenderType { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Relative { get; set; }
        public int? Age { get; set; }
    }
}