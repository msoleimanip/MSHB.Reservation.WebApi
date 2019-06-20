using System.Collections.Generic;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages;


namespace Reservation.WebUI.Layers.L00_BaseModels.Settings
{
    public class RequestResultViewModel
    {
        public object Data { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<ReservationErrorMessage> DetailErrorList { get; set; }=new List<ReservationErrorMessage>();
        public Dictionary<string, string> ValidationMessages { get; set; }

    }
}
