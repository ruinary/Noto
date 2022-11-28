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
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        Int16 teamId;
        string teamName;

        public TeamUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();
        }

        public TeamUC(Int16 _id, string _teamName)
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            this.teamId = _id;
            this.teamName = _teamName;

            teamNameBlock.Text = teamName; 

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT TeamIcon FROM DBNoto.UserTeam_view WHERE TeamID = " + teamId.ToString();
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(reader.GetValue(0) as byte[]);
                    image.EndInit();
                    teamIconCircle.ImageSource = image;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            con.Close();
        }

        private void OpenTeamPageButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.CurrentTeam.teamId = teamId;
            DataWorker.CurrentTeam.teamName = teamName;
            DataWorker.CurrentPage.currentPage = new TeamPage();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
           
        }

        //private void editAlbum_Click(object sender, RoutedEventArgs e)
        //{
        //    UpdateAlbum updateWin = new UpdateAlbum(this.albumId, this.artist, this.album);
        //    updateWin.Show();
        //    Application.Current.Windows[0].Hide();
        //}

        //private void showSongs_Click(object sender, RoutedEventArgs e)
        //{
        //    ShowSongsByAlbum show = new ShowSongsByAlbum(artist, album);
        //    show.Show();
        //    Application.Current.Windows[0].Hide();
        //}
    }
}
