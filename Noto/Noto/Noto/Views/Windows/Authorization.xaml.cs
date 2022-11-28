using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Windows;
using Noto.Data;

namespace Noto.Views.Windows
{
    public partial class Authorization : Window
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=DBNoto;PASSWORD=Pa$$w0rd";
        public Authorization()
        {
            InitializeComponent();
            con.ConnectionString = connectionString;
        }

        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.CurrentUserId = 0;
            DataWorker.UserProfile.userIcon = "D:\\icon.jpg";
            DataWorker.UserProfile.userId = 0;
            DataWorker.UserProfile.userLogin = "loh";
            DataWorker.UserProfile.userEmail = "loh";
            DataWorker.UserProfile.userName = "loh";
            DataWorker.UserProfile.userLastName = "loh";

            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось подключиться к БД");
            }


            //cmd.CommandText = "DBNoto.LOG_IN_USER";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("P_USER_LOGIN", OracleDbType.Varchar2, 30).Value = textBoxLogin.Text.Trim();
            //cmd.Parameters.Add("P_USER_PASSWORD", OracleDbType.Varchar2, 30).Value = textBoxPassword.Text.Trim();
            //cmd.Parameters.Add("O_USER_ID", OracleDbType.Int32, 10);
            //cmd.Parameters["O_USER_ID"].Direction = ParameterDirection.Output;
            //cmd.Parameters.Add("O_USER_LOGIN", OracleDbType.Varchar2, 30);
            //cmd.Parameters["O_USER_LOGIN"].Direction = ParameterDirection.Output;
            //cmd.Parameters.Add("O_USER_ROLE", OracleDbType.Varchar2, 30);
            //cmd.Parameters["O_USER_ROLE"].Direction = ParameterDirection.Output;

            try
            {
                //cmd.ExecuteNonQuery();
                //string user = Convert.ToString(cmd.Parameters["O_USER_LOGIN"].Value);
                //string role = Convert.ToString(cmd.Parameters["O_USER_ROLE"].Value);

                //int id = Convert.ToInt32((decimal)(OracleDecimal)(cmd.Parameters["O_USER_ID"].Value));

                //DataWorker.CurrentUserLogin = user;
                //DataWorker.CurrentUserRole = role;
                //DataWorker.CurrentUserId = id;
                //MessageBox.Show("Пользователь: " + user + "; роль: " + role + "; id: " + id.ToString());

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
            catch (Exception exc)
            {
                MessageBox.Show("Неверный логин или пароль");
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
