using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class CityAttachmentViewModel
    {
        public long Id { get; set; }
        public long CityId { get; set; }
        public string FileType { get; set; }
        public long? FileSize { get; set; }
        public string UrlFile { get; set; }
    }
}
