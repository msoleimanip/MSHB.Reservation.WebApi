using MSHB.Reservation.Layers.L00_BaseModels.Search;
using System.ComponentModel.DataAnnotations;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ReservationUserAttachmentSearchFormModel: SearchModel
    {
        [Required(ErrorMessage = "بایستی همراه مورد نظر انتخاب گردد.")]
        public long? AccommodationUserAttachmentId { get; set; }
        public string NationalCode { get; set; }

    }
}