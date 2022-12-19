using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;

namespace Noto.Views.Windows
{
    public partial class Registration : Window
    {
        OracleConnection con = new OracleConnection();
         
        public Registration()
        {
            InitializeComponent();
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
        }

        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NewUserLogin.Text.Trim()) || String.IsNullOrWhiteSpace(NewUserPassword.Text.Trim()) || String.IsNullOrWhiteSpace(NewUserRepeatPassword.Text.Trim()))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if (NewUserPassword.Text.Trim() != NewUserRepeatPassword.Text.Trim())
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.register_user";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = NewUserLogin.Text.Trim();
            cmd.Parameters.Add("p_user_password", OracleDbType.Varchar2, 30).Value = NewUserPassword.Text.Trim();
            cmd.Parameters.Add("p_user_name", OracleDbType.Varchar2, 30).Value = NewUserName.Text.Trim();
            cmd.Parameters.Add("p_user_lastname", OracleDbType.Varchar2, 30).Value = NewUserLastName.Text.Trim();
            cmd.Parameters.Add("p_user_phonenumber", OracleDbType.Varchar2, 30).Value = NewUserPhoneNumber.Text.Trim();
            cmd.Parameters.Add("p_user_email", OracleDbType.Varchar2, 30).Value = NewUserEmail.Text.Trim();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь успешно создан");

                Authorization authorizationWindow = new Authorization();
                authorizationWindow.Show();
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            con.Close();
        }
        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            Authorization authorizationWindow = new Authorization();
            authorizationWindow.Show();
            this.Close();
        }
    }

}
