using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeTeamNameUC : UserControl
    {

        OracleConnection con = new OracleConnection();
         
        public ChangeTeamNameUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            teamNameBlock.Text = DataWorker.CurrentTeam.teamName;
            editedTeamName.Text = DataWorker.CurrentTeam.teamName;
        }

        private void EditTeamNameButtonClick(object sender, RoutedEventArgs e)
        {
            EditTeamName.Visibility = Visibility.Visible;
            CurTeamName.Visibility = Visibility.Hidden;
        }

        private void ConfTeamNameButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_team_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_team_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;
                cmd.Parameters.Add("p_new_team_name", OracleDbType.Varchar2, 30).Value = editedTeamName.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentTeam.teamName = editedTeamName.Text.Trim();

            EditTeamName.Visibility = Visibility.Hidden;
            CurTeamName.Visibility = Visibility.Visible;

            teamNameBlock.Text = DataWorker.CurrentTeam.teamName;
        }
    }
}
