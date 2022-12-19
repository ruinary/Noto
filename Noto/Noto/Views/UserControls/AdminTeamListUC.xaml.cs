using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class AdminTeamListUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        int rowMargin = 2, rowCounter = 1;
        public AdminTeamListUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();
            loadSomeTeams();
        }

        #region teams
        private void showNextPageTeamsButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter += rowMargin;
            rowCounter++;
            loadSomeTeams();
        }

        private void showBackPageTeamsButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter -= rowMargin;
            rowCounter--;
            loadSomeTeams();
        }

        private void exportTeamsButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.teams_export";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось экспортировать данные!");
            }
        }

        private void importTeamsButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.teams_import";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось импортировать данные!");
            }
        }

        public void loadSomeTeams()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select TeamID, TeamName, row_number() over (ORDER BY TeamName ASC) rn from DBNoto.TeamTable) where rn between :n and :m ORDER BY TeamName ASC";
            cmd.Parameters.Add(new OracleParameter("n", rowCounter));
            cmd.Parameters.Add(new OracleParameter("m", rowCounter + rowMargin));
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            teamList.Children.Clear();
            while (reader.Read())
            {
                TeamUC team = new TeamUC(reader.GetInt16(0), reader.GetString(1));
                teamList.Children.Add(team);
            }
            con.Close();
        }
        #endregion
    }
}
