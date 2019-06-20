using MSHB.Reservation.Layers.L00_BaseModels.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class SearchUserFormModel: SearchModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
      
    }
}
