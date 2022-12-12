﻿using Noto.Data;
using Noto.Views.ExtraWindows;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            ImageWorker.LoadUser1ImageBrush();
            
            ImageBrush brush = new ImageBrush(DataWorker.CurrentTeam.user1IconImg);

            circleUser1.Fill = brush;
            circleUser2.Fill = brush;
            circleUser3.Fill = brush;

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
