using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TeamUC.xaml
    /// </summary>
    public partial class TeamUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=192.168.2.10:1521/orcl;PERSIST SECURITY INFO=True;USER ID=DBMOONYFM;PASSWORD=Pa$$w0rd";
        Int16 year, albumId;
        string album, artist;
        byte[] cover;

        public TeamUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();
        }

        //public TeamUC(Int16 id, string artistName, string albumName, Int16 yearReleased)
        //{
        //    con.ConnectionString = connectionString;
        //    InitializeComponent();
        //    this.albumId = id;
        //    this.album = albumName;
        //    this.artist = artistName;
        //    this.year = yearReleased;

        //    blockAlbumName.Text = albumName;
        //    blockArtistName.Text = artistName;
        //    blockYear.Text = yearReleased.ToString();

        //    con.Open();
        //    OracleCommand cmd = con.CreateCommand();
        //    cmd.CommandText = "SELECT album_blob FROM DBMOONYFM.artist_album_view WHERE album_id = " + id.ToString();
        //    cmd.CommandType = CommandType.Text;
        //    OracleDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        try
        //        {
        //            BitmapImage image = new BitmapImage();
        //            image.BeginInit();
        //            image.StreamSource = new MemoryStream(reader.GetValue(0) as byte[]);
        //            image.EndInit();
        //            albumCover.Source = image;
        //        }
        //        catch (Exception exc)
        //        {
        //            MessageBox.Show(exc.Message);
        //        }
        //    }
        //}

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
