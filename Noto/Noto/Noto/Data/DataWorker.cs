namespace Noto.Data
{
    public static class DataWorker
    {
        public static int CurrentUserId;
        public static string CurrentUserRole;
        public static string CurrentUserLogin;
        public static string CurrentPassword;
        public static string CurrentUserName;
        public static string CurrentUserLastName;
        public static string CurrentUserEmail;
        public static string CurrentUserPhoneNumber;
        public static string CurrentUserIcon;

        public static class UserProfile 
        {
            public static int userId { get; set; }
            public static string userName { get; set; }
            public static string userLastName { get; set; }
            public static string userLogin { get; set; }
            public static string userEmail { get; set; }
            public static string userPhoneNumber { get; set; }
            public static string userIcon { get; set; }

        }

       
    }
}
