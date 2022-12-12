using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;

namespace Noto.Views.ExtraWindows
{
    public partial class TaskSettings : Window
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public TaskSettings()
        {
            con.ConnectionString = connectionString;

            InitializeComponent();
        }

        private void DeleteTaskButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.delete_task";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Задание успешно удалено");

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            con.Close();
        }
    }
}
