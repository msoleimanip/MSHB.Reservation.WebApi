using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class CityViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long ProvinceId { get; set; }
    }
}
