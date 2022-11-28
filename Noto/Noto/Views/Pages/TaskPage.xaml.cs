using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class TaskPage : Page
    {
        OracleConnection con = new OracleConnection();
        public TaskPage()
        {
            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            con.ConnectionString = connectionString;
            MessageBox.Show(DataWorker.CurrentTeam.teamId.ToString());
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM DBNoto.TaskTeam_view WHERE TeamID = " + DataWorker.CurrentTeam.teamId + " ORDER BY TaskPriorityName ASC";
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            //taskList.Children.Clear();
            while (reader.Read())
            {
                TaskUC task = new TaskUC(reader.GetInt16(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                taskList.Children.Add(task);
            }
            con.Close();

            InitializeComponent();
        }
    }

}
