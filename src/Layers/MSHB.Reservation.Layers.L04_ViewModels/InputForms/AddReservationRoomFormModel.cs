using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AddReservationRoomFormModel
    {
        public long? AccommodationRoomId { get; set; }
        [Required(ErrorMessage = "تاریخ ورود الزامی است")]
        public DateTime? EntranceTime { get; set; }
        [Required(ErrorMessage = "تاریخ خروج الزامی است")]
        public DateTime? EndTime { get; set; }
        [Required(ErrorMessage = "کد ملی الزامی است"), MaxLength(12)]
        public string NationalCode { get; set; }
        [Required(ErrorMessage = "شماره تلفن الزامی است"), MaxLength(15)]
        public string PhoneNumber { get; set; }
        public GenderType GenderType { get; set; }
        public string PersonalCode { get; set; }
        public int GuestCounts { get; set; }
        public long Description { get; set; }
    }
}
