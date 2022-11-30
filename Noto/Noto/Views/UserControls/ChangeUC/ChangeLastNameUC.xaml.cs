using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeLastNameUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public ChangeLastNameUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            userLastName.Text = DataWorker.UserProfile.userLastName;
            EditedLastName.Text = DataWorker.UserProfile.userLastName;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                EditProfileLastName.Visibility = Visibility.Visible;
            }
            else
            {
                EditProfileLastName.Visibility = Visibility.Hidden;
            }
        }

        #region Edit Name
        private void EditLastNameButtonClick(object sender, RoutedEventArgs e)
        {
            EditLastName.Visibility = Visibility.Visible;
            CurLastName.Visibility = Visibility.Hidden;
        }
        private void ConfLastNameButtonClick(object sender, RoutedEventArgs e)
        {

            string curPhone = EditedLastName.Text;
            int userAuthId = DataWorker.UserProfile.userId;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_user_last_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                cmd.Parameters.Add("p_new_user_last_name", OracleDbType.Varchar2, 13).Value = EditedLastName.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentUser.currentUserLastName = EditedLastName.Text.Trim();
            DataWorker.UserProfile.userLastName = EditedLastName.Text.Trim();

            EditLastName.Visibility = Visibility.Hidden;
            CurLastName.Visibility = Visibility.Visible;

            userLastName.Text = DataWorker.UserProfile.userLastName;
        }
        #endregion
    }
}
