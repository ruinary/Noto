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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noto.Views.ExtraWindows
{
    public partial class TeamSettings : Window
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        int rowMargin = 4, rowCounter = 1;

        public TeamSettings()
        {
            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmd0 = con.CreateCommand();
            cmd0.CommandText = "ALTER SESSION SET \"_ORACLE_SCRIPT\" = TRUE";
            cmd0.CommandType = CommandType.Text;
            cmd0.ExecuteNonQuery();
            con.Close();

            InitializeComponent();

            ImageWorker.LoadTeamImageBrush();
            teamIconCircle.ImageSource = DataWorker.CurrentTeam.teamIconImg;

            loadSomeUsers();
        }

        public void loadSomeUsers()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select UserID, UserLogin, UserTeamPrivName, row_number() over (order by UserLogin) rn from DBNoto.UserTeam_view WHERE TeamID = '"+DataWorker.CurrentTeam.teamId +"') where rn between :n and :m ORDER BY UserLogin ASC";
            cmd.Parameters.Add(new OracleParameter("n", rowCounter));
            cmd.Parameters.Add(new OracleParameter("m", rowCounter + rowMargin));

            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            userList.Children.Clear();
            while (reader.Read())
            {
                UserUC user = new UserUC(reader.GetInt16(0), reader.GetString(1), reader.GetString(2));
                userList.Children.Add(user);
            }
            con.Close();
        }

        private void showNextPageUsersButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter += rowMargin;
            rowCounter++;
            loadSomeUsers();
        }
        private void showBackPageUsersButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter -= rowMargin;
            rowCounter--;
            loadSomeUsers();
        } 

        private void DeleteTeamButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.delete_team";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Команда успешно удалена");

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            con.Close();
        }

        private void ChangeTeamIconButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ImageWorker.UpdateTeamImageBrush();
                teamIconCircle.ImageSource = DataWorker.CurrentTeam.teamIconImg;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}
