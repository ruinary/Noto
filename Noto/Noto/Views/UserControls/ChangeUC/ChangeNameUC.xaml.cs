using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeNameUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public ChangeNameUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            userName.Text = DataWorker.UserProfile.userName;
            EditedName.Text = DataWorker.UserProfile.userName;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                EditProfileName.Visibility = Visibility.Visible;
            }
            else
            {
                EditProfileName.Visibility = Visibility.Hidden;
            }
        }

        #region Edit Name
        private void EditNameButtonClick(object sender, RoutedEventArgs e)
        {
            EditName.Visibility = Visibility.Visible;
            CurName.Visibility = Visibility.Hidden;
        }
        private void ConfNameButtonClick(object sender, RoutedEventArgs e)
        {

            string curPhone = EditedName.Text;
            int userAuthId = DataWorker.UserProfile.userId;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_user_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                cmd.Parameters.Add("p_new_user_name", OracleDbType.Varchar2, 13).Value = EditedName.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentUser.currentUserName = EditedName.Text.Trim();
            DataWorker.UserProfile.userName = EditedName.Text.Trim();

            EditName.Visibility = Visibility.Hidden;
            CurName.Visibility = Visibility.Visible;

            userName.Text = DataWorker.UserProfile.userName;
        }
        #endregion
    }
}
