using System;
using System.Collections.Generic;
using System.Linq;

using MSHB.Reservation.Layers.L01_Entities.Models;

namespace MSHB.Reservation.Layers.L03_Services.Initialization
{ 
    public static class CustomRoles
    {
        public static List<Role> GetInitialRoles()
        {
            var initRoles = new List<Role>();
            initRoles.AddRange(DefineLog());
            initRoles.AddRange(DefineUserRole());
            initRoles.AddRange(DefineGroupRole());


            return initRoles;
        }
        private static List<Role> DefineLog()
        {
            var initRoles = new List<Role>
            {
                DefineIntRole("Log-Show", "گزارش لاگ")
            };

            return initRoles;

        }
        private static List<Role> DefineUserRole()
        {
            var initRoles = new List<Role>
            {
                DefineIntRole("User-Show", "نمایش کاربران"),
                DefineIntRole("User-Define", "تعریف کاربر"),
                DefineIntRole("User-ResetPassword", "تغییر کلمه عبور کاربر"),
                DefineIntRole("User-Modify", "بروزرسانی کاربر"),
                DefineIntRole("User-Profile", "صفحه شخصی کاربر"),
                DefineIntRole("User-Roles", "تعیین مجوز کاربر"),
                DefineIntRole("User-NewPassword", "ایجاد کلمه عبور جدید"),
                DefineIntRole("User-Delete", "حذف کاربر")
            };

            return initRoles;

        }
        private static List<Role> DefineGroupRole()
        {
            var initRoles = new List<Role>
            {
                DefineIntRole("Group-Show", "نمایش گروه کاربری"),
                DefineIntRole("Group-Define", "تعریف گروه کاربری"),
                DefineIntRole("Group-Delete", "حذف گروه کاربری"),
                DefineIntRole("Group-Modify", "بروزرسانی گروه کاربری")
            };

            return initRoles;

        }
        private static Role DefineIntRole(string name,string title)
        {
            var role = new Role()
            {
                Name = name,
                Title = title,
                Discriminator = "Role"
            };
            
            return role;

        }
    }
}
