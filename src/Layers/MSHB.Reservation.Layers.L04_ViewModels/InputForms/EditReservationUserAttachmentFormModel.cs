using System.ComponentModel.DataAnnotations;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class EditReservationUserAttachmentFormModel:AddReservationUserAttachmentFormModel
    {

        [Required(ErrorMessage = "بایستی همراه مورد نظر انتخاب گردد.")]
        public long? AccommodationUserAttachmentId { get; set; }
        
    }
}