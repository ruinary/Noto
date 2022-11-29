using Noto.Views.Pages;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Data
{
    public static class DataWorker
    {
        public static class CurrentPage
        {
            public static Page currentPage = new UserTeams();
            public static Page currentTaskPage = new TaskPage();
        }

        public static class CurrentTeam
        {
            public static int teamId { get; set; }
            public static string teamName { get; set; }
            public static BitmapImage currentUserIconImg { get; set; }
        }

        public static class CurrentTask
        {
            public static int taskId { get; set; }
            public static string taskTitle { get; set; }
            public static string taskDeadlineDate { get; set; }
            public static string taskCreationDate { get; set; }
            public static string taskPriority { get; set; }
            public static string taskStatus { get; set; }
            public static string taskDescription { get; set; }
        }

        public static class CurrentUser
        {
            public static int currentUserId;
            public static string currentUserRole;
            public static string currentUserLogin;
            public static string currentPassword;
            public static string currentUserName;
            public static string currentUserLastName;
            public static string currentUserEmail;
            public static string currentUserPhoneNumber;
            public static byte[] currentUserIcon;
            public static BitmapImage currentUserIconImg;
        }

        public static class UserProfile 
        {
            public static int userId { get; set; }
            public static string userName { get; set; }
            public static string userLastName { get; set; }
            public static string userLogin { get; set; }
            public static string userEmail { get; set; }
            public static string userPhoneNumber { get; set; }
            public static byte[] userIcon { get; set; }
            public static BitmapImage userIconImg { get; set; }
        }
    }
}
