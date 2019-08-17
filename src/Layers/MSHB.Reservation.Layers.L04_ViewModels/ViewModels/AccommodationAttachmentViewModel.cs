using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class AccommodationAttachmentViewModel
    {
        public long Id { get; set; }
        public long AccommodationId { get; set; }
        public string FileType { get; set; }
        public long? FileSize { get; set; }
        public Guid? FileId { get; set; }
    }
}
