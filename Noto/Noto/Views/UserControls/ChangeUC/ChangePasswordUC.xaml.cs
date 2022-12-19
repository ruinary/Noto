using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangePasswordUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public ChangePasswordUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();
        }

        #region Edit Password
        private void EditPasswordButtonClick(object sender, RoutedEventArgs e)
        {
            bool check1 = !(String.IsNullOrWhiteSpace(textBoxOldPassword.Text.Trim()) || String.IsNullOrWhiteSpace(textBoxPassword.Text.Trim()));
            bool check2 = (DataWorker.CurrentUser.currentPassword == textBoxOldPassword.Text.Trim());
            MessageBox.Show(check1.ToString() + check2.ToString());

            if (check1 && check2)
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                try
                {
                    cmd.CommandText = "DBNoto.update_user_password";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                    cmd.Parameters.Add("p_new_user_password", OracleDbType.Varchar2, 13).Value = textBoxPassword.Text.Trim();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                con.Close();

                DataWorker.CurrentUser.currentPassword = textBoxPassword.Text.Trim();

                textBoxOldPassword.Clear();
                textBoxPassword.Clear();
            }
        }
        #endregion
    }
}
