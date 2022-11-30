using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeLoginUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public ChangeLoginUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            userLogin.Text = DataWorker.UserProfile.userLogin;
            EditedLogin.Text = DataWorker.UserProfile.userLogin;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                EditProfileLogin.Visibility = Visibility.Visible;
            }
            else
            {
                EditProfileLogin.Visibility = Visibility.Hidden;
            }
        }

        #region Edit Login
        private void EditLoginButtonClick(object sender, RoutedEventArgs e)
        {
            EditLogin.Visibility = Visibility.Visible;
            CurLogin.Visibility = Visibility.Hidden;
        }
        private void ConfLoginButtonClick(object sender, RoutedEventArgs e)
        {

            string curPhone = EditedLogin.Text;
            int userAuthId = DataWorker.UserProfile.userId;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_user_login";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                cmd.Parameters.Add("p_new_user_login", OracleDbType.Varchar2, 13).Value = EditedLogin.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentUser.currentUserLogin = EditedLogin.Text.Trim();
            DataWorker.UserProfile.userLogin = EditedLogin.Text.Trim();

            EditLogin.Visibility = Visibility.Hidden;
            CurLogin.Visibility = Visibility.Visible;

            userLogin.Text = DataWorker.UserProfile.userLogin;
        }
        #endregion
    }
}
