using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
   
    public class AddUserFormModel
    {
        
        [Required(ErrorMessage = "باید نام کاربری مشخص گردد")]       
        public string Username { get; set; }
        [Required(ErrorMessage = "باید کلمه عبور مشخص گردد")]
        public string Password { get; set; }

        [MaxLength(500)]
        public string FirstName { get; set; }
        [MaxLength(500)]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public int? IsPresident { get; set; }

        [Required(ErrorMessage = "باید گروه دسترسی کاربر مشخص گردد")]
        public long GroupAuthId { get; set; }

        //[Required(ErrorMessage = "باید شهر کاربر مشخص گردد")]
        //public long CityId { get; set; }
    }
}
