using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class GetAccommodationUnitsViewModel
    {
        public GetAccommodationUnitsViewModel()
        {
            Accommodation = new AccommodationViewModel();
            AccommodationUnits = new List<AccommodationUnitViewModel>();
        }

        public AccommodationViewModel Accommodation { get; set; }
        public List<AccommodationUnitViewModel> AccommodationUnits { get; set; }
    }
}
