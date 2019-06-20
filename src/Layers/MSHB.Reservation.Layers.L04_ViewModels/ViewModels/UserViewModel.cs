using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }    
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int? IsPresident { get; set; }
        public long? GroupAuthId { get; set; }
        public long? CityId { get; set; }
        public long? UserConfigurationId { get; set; }
        public DateTime? LastLockoutDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastVisit { get; set; }       
        public DateTimeOffset? LastLoggedIn { get; set; }
    }
}
