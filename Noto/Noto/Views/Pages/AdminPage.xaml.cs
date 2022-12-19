using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class AdminPage : Page
    {
        OracleConnection con = new OracleConnection();
        public AdminPage()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();
        }

        private void showUsersButtonClick(object sender, RoutedEventArgs e)
        {
            teamsGrid.Visibility = Visibility.Hidden;
            usersGrid.Visibility = Visibility.Visible;
        }

        private void showTeamsButtonClick(object sender, RoutedEventArgs e)
        {
            usersGrid.Visibility = Visibility.Hidden;
            teamsGrid.Visibility = Visibility.Visible;
        }

       
    }
}
