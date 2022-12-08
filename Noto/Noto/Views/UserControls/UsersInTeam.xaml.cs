using Noto.Data;
using System;
using Microsoft.Win32;
using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Oracle.ManagedDataAccess.Types;
using Noto.Views.ExtraWindows;

namespace Noto.Views.UserControls
{
    public partial class UsersInTeam : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public UsersInTeam()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            //BitmapImage image = new BitmapImage();
            //image.BeginInit();
            //image.StreamSource = new MemoryStream(DataWorker.UserProfile.userIcon as byte[]);
            //image.EndInit();
            //ImageBrush brush = new ImageBrush(image);
            //circleUser1.Fill = brush;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.count_user_in_team";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_team_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;

            cmd.Parameters.Add("o_count_users", OracleDbType.Int32, 10);
            cmd.Parameters["o_count_users"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            int count = Convert.ToInt32((decimal)(OracleDecimal)(cmd.Parameters["o_count_users"].Value));

            CountTeamates.Text = count.ToString();
            con.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUserInTeam auit = new AddUserInTeam();
            auit.Show();
        }
    }
}
