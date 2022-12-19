using Noto.Data;
using Noto.Views.Pages;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Views.UserControls
{
    public partial class TeamUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        Int16 teamId;
        string teamName;

        public TeamUC()
        {
            InitializeComponent();
        }

        public TeamUC(Int16 _id, string _teamName)
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            this.teamId = _id;
            this.teamName = _teamName;

            DataWorker.CurrentTeam.teamId = _id;

            teamNameBlock.Text = teamName;
            ImageWorker.LoadTeamImageBrush();
            teamIconCircle.ImageSource = DataWorker.CurrentTeam.teamIconImg;
        }

        #region Open Current Team Page
        private void OpenTeamPageButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.CurrentTeam.teamId = teamId;
            DataWorker.CurrentTeam.teamName = teamName;
            DataWorker.CurrentPage.currentPage = new TeamPage();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
           
        }
        #endregion
    }
}
