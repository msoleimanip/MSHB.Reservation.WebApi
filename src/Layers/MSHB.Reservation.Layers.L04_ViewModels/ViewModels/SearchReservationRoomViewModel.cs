using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class SearchReservationRoomViewModel:GeneralViewModel
    {
        public List<ReservationRoomViewModel> searchReservationRoomViewModels { get; set; }
    }
}
