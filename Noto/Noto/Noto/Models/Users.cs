using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noto.Models
{
    public class Users
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string userLogin { get; set; }
        public string userEmail { get; set; }
        public string userPhoneNumber { get; set; }
        public string userIcon { get; set; }

        public static List<Users> users = new List<Users>();

        public Users() { }
        public Users(int _userId, string _userName, string _userLastName, string _userLogin, string _userEmail, string _userPhoneNumber, string _userIcon) 
        {
            this.userId = _userId;
            this.userName = _userName;
            this.userLastName = _userLastName;
            this.userLogin = _userLogin;
            this.userEmail = _userEmail;
            this.userPhoneNumber = _userIcon;
        }
        public void AddUser(int _userId, string _userName, string _userLastName, string _userLogin, string _userEmail, string _userPhoneNumber, string _userIcon)
        {
            Users newuser = new Users(_userId, _userName, _userLastName, _userLogin, _userEmail, _userPhoneNumber, _userIcon);
            users.Add(newuser);
        }
    }
}
