using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SubPro.WebUI.Shared.Common.IdentityToolkit
{
    public static class IdentityExtensions
    {

        public static List<string> GetUserClaimRoles(this IIdentity identity)
        {
            var identity1 = identity as ClaimsIdentity;
            return identity1?.GetUserRoleClaimValue(ClaimTypes.Role);
        }
        public static List<string> GetUserRoleClaimValue(this IIdentity identity, string claimType)
        {
            var identity1 = identity as ClaimsIdentity;
            return identity1?.FindAllValue(claimType);
        }
        public static List<string> FindAllValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindAll(claimType) as List<string>;
        }
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            return identity?.FindFirst(claimType)?.Value;
        }

        public static string GetUserClaimValue(this IIdentity identity, string claimType)
        {
            var identity1 = identity as ClaimsIdentity;
            return identity1?.FindFirstValue(claimType);
        }

        public static string GetUserFirstName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.GivenName);
        }

        //public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        public static T GetUserId<T>(this IIdentity identity) 
        {
            var firstValue = identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
            return firstValue != null
                ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(firstValue)
                : default(T);
        }

        public static string GetUserId(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
        }
        public static T GetUserPresident<T>(this IIdentity identity)
        {
            var isPresident=identity?.GetUserClaimValue("IsPresident");
            return isPresident != null
                ? (T)Convert.ChangeType(isPresident, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
           
        }
        public static string GetUserLastName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Surname);
        }

        public static string GetUserFullName(this IIdentity identity)
        {
            return $"{GetUserFirstName(identity)} {GetUserLastName(identity)}";
        }

        public static string GetUserDisplayName(this IIdentity identity)
        {
            var fullName = GetUserFullName(identity);
            return string.IsNullOrWhiteSpace(fullName) ? GetUserName(identity) : fullName;
        }

        public static string GetUserName(this IIdentity identity)
        {
            return identity?.GetUserClaimValue(ClaimTypes.Name);
        }
    }
}