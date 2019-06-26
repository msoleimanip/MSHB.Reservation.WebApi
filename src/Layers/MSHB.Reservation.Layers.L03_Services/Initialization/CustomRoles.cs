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
            initRoles.AddRange(DefineInitRole());


            return initRoles;
        }

        private static List<Role> DefineInitRole()
        {
            var initRoles = new List<Role>
            {
                DefineIntRole("File", "File"),
                DefineIntRole("File-UploadFile", "File-UploadFile"),
                DefineIntRole("File-DownloadFile", "File-DownloadFile"),


                DefineIntRole("Reservation", "Reservation"),
                DefineIntRole("Reservation-AddReservation", "Reservation-AddReservation"),
                DefineIntRole("Reservation-EditReservation", "Reservation-EditReservation"),
                DefineIntRole("Reservation-DeleteReservation", "Reservation-DeleteReservation"),
                DefineIntRole("Reservation-GetUserReservation", "Reservation-GetUserReservation"),

                
                DefineIntRole("ReservationUserAttachment", "ReservationUserAttachment"),
                DefineIntRole("ReservationUserAttachment-AddReservationUserAttachment", "ReservationUserAttachment-AddReservationUserAttachment"),
                DefineIntRole("ReservationUserAttachment-EditReservationUserAttachment", "ReservationUserAttachment-EditReservationUserAttachment"),
                DefineIntRole("ReservationUserAttachment-DeleteReservationUserAttachment", "ReservationUserAttachment-DeleteReservationUserAttachment"),
                DefineIntRole("ReservationUserAttachment-GetReservationUserAttachment", "ReservationUserAttachment-GetReservationUserAttachment"),


                DefineIntRole("GroupAuthentication", "GroupAuthentication"),
                DefineIntRole("GroupAuthentication-GetGroupAuthentication", "GroupAuthentication-GetGroupAuthentication"),
                DefineIntRole("GroupAuthentication-GetGroupRole", "GroupAuthentication-GetGroupRole"),
                DefineIntRole("GroupAuthentication-AddGroup", "GroupAuthentication-AddGroup"),
                DefineIntRole("GroupAuthentication-EditGroup", "GroupAuthentication-EditGroup"),
                DefineIntRole("GroupAuthentication-DeleteGroup", "GroupAuthentication-DeleteGroup"),
                DefineIntRole("GroupAuthentication-GetRoles", "GroupAuthentication-GetRoles"),


                DefineIntRole("City", "City"),
                DefineIntRole("City-Get", "City-Get"),
                DefineIntRole("City-GetCityByUser", "City-GetCityByUser"),
                DefineIntRole("City-GetUserCityForUser", "City-GetUserCityForUser"),
                DefineIntRole("City-AddCity", "City-AddCity"),
                DefineIntRole("City-EditCity", "City-EditCity"),
                DefineIntRole("City-DeactivateCity", "City-DeactivateCity"),
                DefineIntRole("City-DeleteCity", "City-DeleteCity"),
                DefineIntRole("City-SetCityLocation", "City-SetCityLocation"),
                DefineIntRole("City-SetCityImages", "City-SetCityImages"),
                DefineIntRole("City-DeleteAttachmentCity", "City-DeleteAttachmentCity"),


                DefineIntRole("Account", "Account"),
                DefineIntRole("Account-RefreshToken", "Account-RefreshToken"),
                DefineIntRole("Account-AddUser", "Account-AddUser"),
                DefineIntRole("Account-EditUser", "Account-EditUser"),
                DefineIntRole("Account-ChangeActivateUser", "Account-ChangeActivateUser"),
                DefineIntRole("Account-ChangePassword", "Account-ChangePassword"),
                DefineIntRole("Account-GetUsers", "Account-GetUsers"),
                DefineIntRole("Account-UserCityAssign", "Account-UserCityAssign"),
                DefineIntRole("Account-GetUserById", "Account-GetUserById"),



                DefineIntRole("Accommodation", "Accommodation"),
                DefineIntRole("Accommodation-AddAccommodation", "Accommodation-AddAccommodation"),
                DefineIntRole("Accommodation-EditAccommodation", "Accommodation-EditAccommodation"),
                DefineIntRole("Accommodation-DeleteAccommodation", "Accommodation-DeleteAccommodation"),
                DefineIntRole("Accommodation-GetUserAccommodation", "Accommodation-GetUserAccommodation"),
                DefineIntRole("Accommodation-DeactivateAccommodation", "Accommodation-DeactivateAccommodation"),
                DefineIntRole("Accommodation-GetAccommodationById", "Accommodation-GetAccommodationById"),


                DefineIntRole("Report", "Report"),
                

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
