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
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public long? SystemCode { get; set; }
        public string PersonalCode { get; set; }
    }
}
