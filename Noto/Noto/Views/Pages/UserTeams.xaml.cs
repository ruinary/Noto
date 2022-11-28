using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Noto.Views.Pages
{
    public partial class UserTeams : Page
    {
        OracleConnection con = new OracleConnection();
        public UserTeams()
        {
            InitializeComponent();
        }

        public void LoadUserTeams()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DBNoto.UserTeam_view ORDER BY TeamName ASC";
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            //teamList.Children.Clear();
            //while (reader.Read())
            //{
            //    //AlbumUC artist = new AlbumUC(reader.GetString(0), reader.GetString(1), reader.GetInt16(2));
            //    TeamUC artist = new TeamUC(reader.GetInt16(0), reader.GetString(1), reader.GetString(2), reader.GetInt16(3));
            //    teamList.Children.Add(artist);
            //}
            con.Close();
        }
    }
}
