using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class CityViewModel
    {
        public CityViewModel()
        {
            CityDetailAttachments = new List<CityAttachmentViewModel>();
        }
        public long Id { get; set; }
        public string CityName { get; set; }

        public string Description { get; set; }

        public long? ParentId { get; set; }

        public bool? IsActivated { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public Guid? FileId { get; set; }
        public DateTime? DeactiveStartTime { get; set; }

        public List<CityAttachmentViewModel> CityDetailAttachments { get; set; }
    }
}
