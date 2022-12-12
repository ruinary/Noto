using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class Profile : Page
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public Profile()
        {
            InitializeComponent();
            con.ConnectionString = connectionString;

            ImageWorker.LoadUserImageBrush();

            userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                ChangePasswordGrid.Visibility = Visibility.Visible;
                DeleteAccGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePasswordGrid.Visibility = Visibility.Hidden;
                DeleteAccGrid.Visibility = Visibility.Hidden;
            }
        }

        
        #region Edit Photo
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageWorker.UpdateUserImageBrush();
                userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        #endregion

        private void DeleteProfileButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "DBNoto.delete_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.CurrentUser.currentUserLogin;

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Пользователь удален!");
            Application.Current.Windows[0].Close();

        }
    }
}
