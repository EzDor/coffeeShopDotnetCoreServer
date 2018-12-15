using System.Collections.Generic;

namespace WebApplication.Utils
{
    public static class Constants
    {
        public const string USER_ROLE = "ROLE_USER";
        public const string ADMIN_ROLE = "ROLE_ADMIN";
        public const string role_key = "roles";


        public static List<string> UserStatusValues = new List<string>()
        {
            "ACTIVE",
            "DISCARDED"
        };
    }
}