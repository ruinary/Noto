﻿using Noto.Data;
using Noto.Views.UserControls;
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
            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            con.ConnectionString = connectionString;

            InitializeComponent();

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DBNoto.UserTeam_view WHERE UserID = " + DataWorker.CurrentUser.currentUserId + " ORDER BY TeamName ASC";
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
    }
}
