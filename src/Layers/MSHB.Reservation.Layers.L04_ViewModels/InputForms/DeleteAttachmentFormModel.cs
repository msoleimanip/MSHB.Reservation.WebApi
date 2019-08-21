using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class DeleteAttachmentFormModel
    {
        [Required(ErrorMessage = "شناسه میبایستی ارسال گردد")]
        public long AttachmentId { get; set; }
    }
}
