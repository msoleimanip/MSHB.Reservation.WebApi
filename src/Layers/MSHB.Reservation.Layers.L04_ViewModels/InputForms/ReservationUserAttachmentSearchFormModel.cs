using MSHB.Reservation.Layers.L00_BaseModels.Search;
using System.ComponentModel.DataAnnotations;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ReservationUserAttachmentSearchFormModel: SearchModel
    {
        [Required(ErrorMessage = "بایستی کد رزو مورد نظر انتخاب گردد.")]
        public long? AccommodationUserId { get; set; }
        public string NationalCode { get; set; }

    }
}