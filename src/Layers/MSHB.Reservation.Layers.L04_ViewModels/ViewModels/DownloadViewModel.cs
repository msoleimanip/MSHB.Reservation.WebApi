using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class DownloadViewModel
    {

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public MemoryStream Memory { get; set; }
    }
}
