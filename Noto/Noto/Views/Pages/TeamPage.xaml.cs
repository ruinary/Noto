using Noto.Data;
using Noto.Views.ExtraWindows;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Noto.Views.Pages
{
    public partial class TeamPage : Page
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root"; 
        string userRoleInTeam = "";
        public TeamPage()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            TeamName.DataContext = DataWorker.CurrentTeam.teamName;

            DataWorker.CurrentPage.currentTaskPage = new TaskPage();
            taskFrame.DataContext = DataWorker.CurrentPage.currentTaskPage;

            ImageWorker.LoadTeamImageBrush();
            ImageBrush brush = new ImageBrush(DataWorker.CurrentTeam.teamIconImg);
            circleTeamIcon.Fill = brush;

            con.Open();

            try
            {
                OracleCommand cmd = con.CreateCommand();

                cmd.CommandText = "DBNoto.search_user_role_in_team";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentUser.currentUserId;

                cmd.Parameters.Add("o_user_role", OracleDbType.Varchar2, 30);
                cmd.Parameters["o_user_role"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                userRoleInTeam = Convert.ToString(cmd.Parameters["o_user_role"].Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Данные для входа введены неверно!");
            }

            if (userRoleInTeam != "OWNER")
            {
                teamSettingsButton.Visibility = Visibility.Hidden;
            }
            else
            {
                teamSettingsButton.Visibility = Visibility.Visible;
            }
            con.Close();
        }

        private void TeamSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            TeamSettings teamSettings = new TeamSettings();
            teamSettings.Show();
        }

        private void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.CurrentPage.currentTaskPage = new CalendarPage();
            taskFrame.DataContext = DataWorker.CurrentPage.currentTaskPage;
        }
    }
}
