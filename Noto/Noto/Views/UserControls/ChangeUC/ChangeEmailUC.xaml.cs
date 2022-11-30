using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeEmailUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public ChangeEmailUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            userEmail.Text = DataWorker.UserProfile.userEmail;
            EditedEmail.Text = DataWorker.UserProfile.userEmail;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                EditProfileEmail.Visibility = Visibility.Visible;
            }
            else
            {
                EditProfileEmail.Visibility = Visibility.Hidden;
            }
        }

        #region Edit Email
        private void Button_Click_EditEMail(object sender, RoutedEventArgs e)
        {
            EditEmail.Visibility = Visibility.Visible;
            CurEmail.Visibility = Visibility.Hidden;
        }
        private void Button_Click_ConfEMail(object sender, RoutedEventArgs e)
        {
            bool check1 = Regex.IsMatch(EditedEmail.Text, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)");

            if (check1)
            {
                string curEmail = EditedEmail.Text;
                int userAuthId = DataWorker.UserProfile.userId;

                con.Open();
                OracleCommand cmd = con.CreateCommand();
                try
                {
                    cmd.CommandText = "DBNoto.update_user_email";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                    cmd.Parameters.Add("p_new_user_email", OracleDbType.Varchar2, 30).Value = EditedEmail.Text.Trim();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                con.Close();

                DataWorker.CurrentUser.currentUserEmail = EditedEmail.Text.Trim();
                DataWorker.UserProfile.userEmail = EditedEmail.Text.Trim();

                EditEmail.Visibility = Visibility.Hidden;
                CurEmail.Visibility = Visibility.Visible;

                userEmail.Text = DataWorker.UserProfile.userEmail;
            }
            else
            {
                MessageBox.Show("Введенные данные не являются почтой!");
            }
        }
        #endregion

    }
}
