using System;
using Microsoft.AspNetCore.Identity;

namespace MSHB.Reservation.Layers.L00_BaseModels.Settings
{
    public class SiteSettings
    {
        // ReSharper disable once InconsistentNaming
   
        public PageSize SearchSize { get; set; }         
        public UserAttachedFile UserAttachedFile { get; set; }
        public string SeedMode { get; set; } = "PRODUCT";
        public UserSeed AdminUserSeed { get; set; }
        public string SmsUrl { get; set; }
        public Logging Logging { get; set; }
        public Smtp Smtp { get; set; }
        public Connectionstrings ConnectionStrings { get; set; }
        public bool EnableEmailConfirmation { get; set; }
        public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
        public int NotAllowedPreviouslyUsedPasswords { get; set; }
        public int ChangePasswordReminderDays { get; set; }   
        public PasswordOptions PasswordOptions { get; set; }
        public ActiveDatabase ActiveDatabase { get; set; }
        public string UsersAvatarsFolder { get; set; }
        public string UserDefaultPhoto { get; set; }
        public CookieOptions CookieOptions { get; set; }
        public LockoutOptions LockoutOptions { get; set; }
        public UserAvatarImageOptions UserAvatarImageOptions { get; set; }
        public string[] EmailsBanList { get; set; }
        public string[] PasswordsBanList { get; set; }
    }
}