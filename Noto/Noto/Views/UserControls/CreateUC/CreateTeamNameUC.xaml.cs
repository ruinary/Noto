using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.CreateUC
{
    public partial class CreateTeamNameUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public CreateTeamNameUC()
        {
            InitializeComponent();
        }

        private void ConfTeamNameButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.ConnectionString = connectionString;
                InitializeComponent();

                con.Open();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.create_team";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.CurrentUser.currentUserLogin;
                cmd.Parameters.Add("p_team_name", OracleDbType.Varchar2, 30).Value = createTeamName.Text.Trim();

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
