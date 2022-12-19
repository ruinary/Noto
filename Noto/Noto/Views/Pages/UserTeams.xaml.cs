using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class UserTeams : Page
    {
        OracleConnection con = new OracleConnection();
          
        int rowMargin = 2, rowCounter = 1;
        public UserTeams()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;

            InitializeComponent();

            con.Open();
            OracleCommand cmd0 = con.CreateCommand();
            cmd0.CommandText = "ALTER SESSION SET \"_ORACLE_SCRIPT\" = TRUE";
            cmd0.CommandType = CommandType.Text;
            cmd0.ExecuteNonQuery();
            con.Close();

            loadSomeTeams();

            CreateTeamUC newteam = new CreateTeamUC();
            teamList.Children.Add(newteam);
        }

        public void loadSomeTeams()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select TeamID, TeamName, row_number() over (ORDER BY TeamName ASC) rn from DBNoto.UserTeam_view WHERE UserID = " + DataWorker.CurrentUser.currentUserId + ") where rn between :n and :m ORDER BY TeamName ASC";
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

        private void showBackPageUsersButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter -= rowMargin;
            rowCounter--;
            loadSomeTeams();
            CreateTeamUC newteam = new CreateTeamUC();
            teamList.Children.Add(newteam);
        }

        private void showNextPageUsersButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter += rowMargin;
            rowCounter++;
            loadSomeTeams();
            CreateTeamUC newteam = new CreateTeamUC();
            teamList.Children.Add(newteam);
        }
    }
}
