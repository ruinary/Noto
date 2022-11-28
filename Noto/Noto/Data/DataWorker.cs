using System.Collections.Generic;

namespace Noto.Data
{
    public static class DataWorker
    {
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
        }
        public class Team
        {
            public int teamId { get; set; }
            public string teamName { get; set; }
            public byte[] teamIcon { get; set; }

            //public static List<Task> tasks = new List<Task>();

            Team() { }
            Team(int _teamId, string _teamName, byte[] _teamIcon) 
            {
                this.teamId = _teamId;
                this.teamName = _teamName;
                this.teamIcon = _teamIcon;
            }
            Team(int _teamId, string _teamName)
            {
                this.teamId = _teamId;
                this.teamName = _teamName;
                //this.teamIcon = _teamIcon;
            }
        }
        
        public static List<Team> teams = new List<Team>();

        //public class Task
        //{
        //    public int taskId { get; set; }
        //    public string TaskTeamID { get; set; }

        //    public string TaskTeamID { get; set; }

        //    public string TaskTeamID { get; set; }

        //    public string TaskTeamID { get; set; }
        //    public byte[] teamIcon { get; set; }

        //    /*
        //    TaskID NUMBER(10) GENERATED AS IDENTITY(START WITH 1 INCREMENT BY 1),
        //    TaskTeamID NUMBER(10) NOT NULL,
        //    TaskStatus NUMBER(10) NOT NULL,
        //    TaskPriority NUMBER(10) NOT NULL,
        //    TaskTitle VARCHAR2(30) NOT NULL,
        //    TaskDescription VARCHAR2(200) NOT NULL,
        //    CreationDate VARCHAR2(30) NOT NULL,
        //    DeadlineDate VARCHAR2(30) NOT NULL,
        //     */

        //    Task() { }
        //    Task(int _teamId, string _teamName, byte[] _teamIcon)
        //    {
        //        this.teamId = _teamId;
        //        this.teamName = _teamName;
        //        this.teamIcon = _teamIcon;
        //    }
        //    Task(int _teamId, string _teamName)
        //    {
        //        this.teamId = _teamId;
        //        this.teamName = _teamName;
        //        //this.teamIcon = _teamIcon;
        //    }
        //}
    }
}
