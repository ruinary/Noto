using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Windows;
using Noto.Data;
using System.IO;
using System.Windows.Media.Imaging;
using Noto.Views.Pages;

namespace Noto.Views.Windows
{
    public partial class Authorization : Window
    {
        OracleConnection con = new OracleConnection();
         
        public Authorization()
        {
            InitializeComponent();
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
        }

        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();

                try
                {
                    OracleCommand cmd = con.CreateCommand();

                    cmd.CommandText = "DBNoto.LOG_IN_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = textBoxLogin.Text.Trim();
                    cmd.Parameters.Add("p_user_password", OracleDbType.Varchar2, 30).Value = textBoxPassword.Text.Trim();

                    cmd.Parameters.Add("o_user_id", OracleDbType.Int32, 10);
                    cmd.Parameters["o_user_id"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("o_user_login", OracleDbType.Varchar2, 30);
                    cmd.Parameters["o_user_login"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("o_user_role", OracleDbType.Varchar2, 30);
                    cmd.Parameters["o_user_role"].Direction = ParameterDirection.Output;

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
                    string user = Convert.ToString(cmd.Parameters["o_user_login"].Value);
                    string role = Convert.ToString(cmd.Parameters["o_user_role"].Value);
                    string name = Convert.ToString(cmd.Parameters["o_user_name"].Value);
                    string lastname = Convert.ToString(cmd.Parameters["o_user_lastname"].Value);
                    string phonenumber = Convert.ToString(cmd.Parameters["o_user_phonenumber"].Value);
                    string email = Convert.ToString(cmd.Parameters["o_user_email"].Value);

                    DataWorker.CurrentUser.currentUserId = id;
                    DataWorker.CurrentUser.currentPassword = textBoxPassword.Text.Trim();
                    DataWorker.CurrentUser.currentUserLogin = user;
                    DataWorker.CurrentUser.currentUserRole = role;
                    DataWorker.CurrentUser.currentUserEmail = email;
                    DataWorker.CurrentUser.currentUserPhoneNumber = phonenumber;
                    DataWorker.CurrentUser.currentUserName = name;
                    DataWorker.CurrentUser.currentUserLastName = lastname;

                    ImageWorker.LoadUserImageBrush();
                    DataWorker.CurrentUser.currentUserIconImg = DataWorker.UserProfile.userIconImg;

                    DataWorker.CurrentPage.currentPage = new UserTeams();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();

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
        }
            
        private void SigInButtonClick(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            registrationWindow.Show();
            this.Close();
        }
    }
}
