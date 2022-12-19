using System.Windows;
using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Controls;

namespace Noto.Views.ExtraWindows
{
    public partial class AddUserInTeam : Window
    {
        OracleConnection con = new OracleConnection();
         
        public AddUserInTeam()
        {
            InitializeComponent();
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
        }

        private void AddUserToTeamButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.add_user_to_team";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = addedUserLogin.Text.Trim();
            cmd.Parameters.Add("p_team_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;
            
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь успешно добавлен");

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Пользователь уже добавлен!");
            }
            con.Close();
        }
    }
}
