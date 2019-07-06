using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class ReportStructureViewModel
    {
        public long ReportStructureId { get; set; }
        public string ReportId { get; set; }    
        public string Configuration { get; set; }
        public string ProtoType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
