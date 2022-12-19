using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Noto.Views.Pages
{
    public partial class Profile : Page
    {
        OracleConnection con = new OracleConnection();
         
        public Profile()
        {
            InitializeComponent();
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;

            ImageWorker.LoadUserImageBrush();


            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;

                ChangePasswordGrid.Visibility = Visibility.Visible;
                DeleteAccGrid.Visibility = Visibility.Visible;
                changeUserIconGrid.Visibility = Visibility.Visible;
                simpleUserIconGrid.Visibility = Visibility.Hidden;

            }
            else
            {
                ImageBrush brush = new ImageBrush(DataWorker.UserProfile.userIconImg);
                circleUser.Fill = brush;

                ChangePasswordGrid.Visibility = Visibility.Hidden;
                DeleteAccGrid.Visibility = Visibility.Hidden;
                changeUserIconGrid.Visibility = Visibility.Hidden;
                simpleUserIconGrid.Visibility = Visibility.Visible;
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
        #region Delete Profile
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
        #endregion
    }
}
