using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AccommodationUnitFormModel
    {
        [Required(ErrorMessage = "شناسه اقامتگاه وجود ندارد")]
        public long AccommodationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
