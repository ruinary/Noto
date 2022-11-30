using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangePhoneNumberUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public ChangePhoneNumberUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            userPhoneNumber.Text = DataWorker.UserProfile.userPhoneNumber;
            EditedPhone.Text = DataWorker.UserProfile.userPhoneNumber;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                EditProfilePhoneNumber.Visibility = Visibility.Visible;
            }
            else
            {
                EditProfilePhoneNumber.Visibility = Visibility.Hidden;
            }
        }

        #region Edit Phone
        private void Button_Click_EditPhone(object sender, RoutedEventArgs e)
        {
            EditPhone.Visibility = Visibility.Visible;
            CurPhone.Visibility = Visibility.Hidden;
        }

        private void Button_Click_ConfPhone(object sender, RoutedEventArgs e)
        {
            bool check1 = Regex.IsMatch(EditedPhone.Text, @"^(\+375|80)(29|44|33)([0-9]){7}$");
            if (check1)
            {
                string curPhone = EditedPhone.Text;
                int userAuthId = DataWorker.UserProfile.userId;

                con.Open();
                OracleCommand cmd = con.CreateCommand();
                try
                {
                    cmd.CommandText = "DBNoto.update_user_phonenumber";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                    cmd.Parameters.Add("p_new_user_phonenumber", OracleDbType.Varchar2, 13).Value = EditedPhone.Text.Trim();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                con.Close();

                DataWorker.CurrentUser.currentUserPhoneNumber = EditedPhone.Text.Trim();
                DataWorker.UserProfile.userPhoneNumber = EditedPhone.Text.Trim();

                EditPhone.Visibility = Visibility.Hidden;
                CurPhone.Visibility = Visibility.Visible;

                userPhoneNumber.Text = DataWorker.UserProfile.userPhoneNumber;
            }
        }
        #endregion
    }
}
