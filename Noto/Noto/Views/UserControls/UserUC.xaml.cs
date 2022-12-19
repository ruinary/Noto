using Noto.Data;
using Noto.Views.Pages;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Views.UserControls
{
    public partial class UserUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         

        Int16 idUser;
        string userLogin;
        string userTeamPrivName;

        public UserUC()
        {
            InitializeComponent();
        }

        public UserUC(Int16 _idUser, string _userLogin, string _userTeamPrivName)
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            this.idUser = _idUser;
            this.userLogin = _userLogin;
            this.userTeamPrivName = _userTeamPrivName;

            DataWorker.UserProfile.userId = _idUser;
            DataWorker.UserProfile.userLogin = _userLogin;

            ImageWorker.LoadUserImageBrush();

            userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;
            userNameBlock.Text = _userLogin;

            if (DataWorker.CurrentPage.currentPage is AdminPage)
            {
                usereDeleteButton.Visibility = Visibility.Hidden;
                ownerUserIcon.Visibility = Visibility.Hidden;
            }
            if (!(DataWorker.CurrentPage.currentPage is AdminPage))
            {
                if (userTeamPrivName == "OWNER")
                {
                    usereDeleteButton.Visibility = Visibility.Hidden;
                    ownerUserIcon.Visibility = Visibility.Visible;
                }
                else
                {
                    usereDeleteButton.Visibility = Visibility.Visible;
                    ownerUserIcon.Visibility = Visibility.Hidden;
                }
            }
        }
        #region Remove User From Team
        private void RemoveUserFromTeamButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.remove_user_from_team";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_id", OracleDbType.Int32, 10).Value = idUser;
                cmd.Parameters.Add("p_team_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Пользователь удален из команды!");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        #endregion
        #region Open Current User Page
        private void OpenUserPageButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();

                try
                {
                    OracleCommand cmd = con.CreateCommand();

                    cmd.CommandText = "DBNoto.search_user";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = userLogin;
                    
                    cmd.Parameters.Add("o_user_id", OracleDbType.Int32, 10);
                    cmd.Parameters["o_user_id"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("o_user_name", OracleDbType.Varchar2, 30);
                    cmd.Parameters["o_user_name"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("o_user_lastname", OracleDbType.Varchar2, 30);
                    cmd.Parameters["o_user_lastname"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("o_user_phonenumber", OracleDbType.Varchar2, 13);
                    cmd.Parameters["o_user_phonenumber"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("o_user_email", OracleDbType.Varchar2, 30);
                    cmd.Parameters["o_user_email"].Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    int id = Convert.ToInt32((decimal)(OracleDecimal)(cmd.Parameters["o_user_id"].Value));
                    string name = Convert.ToString(cmd.Parameters["o_user_name"].Value);
                    string lastname = Convert.ToString(cmd.Parameters["o_user_lastname"].Value);
                    string phonenumber = Convert.ToString(cmd.Parameters["o_user_phonenumber"].Value);
                    string email = Convert.ToString(cmd.Parameters["o_user_email"].Value); 
                    
                    DataWorker.UserProfile.userId = id;
                    DataWorker.UserProfile.userLogin = userLogin;
                    DataWorker.UserProfile.userEmail = email;
                    DataWorker.UserProfile.userPhoneNumber = phonenumber;
                    DataWorker.UserProfile.userName = name;
                    DataWorker.UserProfile.userLastName = lastname;
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Данные для входа введены неверно!");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось подключиться к БД");
            }

            con.Close();

            DataWorker.CurrentPage.currentPage = new Profile();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
        }
        #endregion
    }
}
