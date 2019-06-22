using MSHB.Reservation.Layers.L00_BaseModels.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ReservationRoomSearchFormModel : SearchModel
    {
        [Required(ErrorMessage = "شناسه اقامتگاه وارد نشده است")]
        public long? CityId { get; set; }
        public List<long> AccommodationUserRoomId{get;set;}
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? SystemCode { get; set; }
        public string PersonalCode { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
