using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class SearchBookinationViewModel : GeneralViewModel
    {
        public List<BookinationViewModel> bookinationViewModels { get; set; }
    }
}
