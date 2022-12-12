using Microsoft.Win32;
using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            ImageWorker.LoadImageBrush();

            userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                ChangePasswordGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePasswordGrid.Visibility = Visibility.Hidden;
            }
        }

        
        #region Edit Photo
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageWorker.UpdateImageBrush();
                userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        #endregion
    }
}
