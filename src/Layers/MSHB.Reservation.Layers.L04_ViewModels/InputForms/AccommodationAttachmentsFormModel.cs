using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AccommodationAttachmentsFormModel
    {
        public long AccommodationId;
        public List<string> UploadFiles { get; set; }
    }
}
