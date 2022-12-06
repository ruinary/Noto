using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeStatusesUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public ChangeStatusesUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            switch (DataWorker.CurrentTask.taskStatus)
            {
                case "NOT STARTED":
                    selectorNotStarted.IsSelected = true;
                    break;
                case "STARTED":
                    selectorStarter.IsSelected = true;
                    break;
                case "IN PROCESS":
                    selectorInProcess.IsSelected = true;
                    break;
                case "READY":
                    selectorReady.IsSelected = true;
                    break;
            }
        }
        private void Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            OracleCommand cmd = con.CreateCommand();
            switch ((prioritySelection.SelectedItem as ListBoxItem).Content.ToString())
            {
                case "NOT STARTED":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_status";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_status", OracleDbType.Int16, 10).Value = 1;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskStatus = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                    }
                    break;
                case "STARTED":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_status";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_status", OracleDbType.Int16, 10).Value = 2;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskStatus = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                    }
                    break;
                case "IN PROCESS":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_status";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_status", OracleDbType.Int16, 10).Value = 3;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskStatus = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                    }
                    break;
                case "READY":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_status";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_status", OracleDbType.Int16, 10).Value = 4;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskStatus = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                    }
                    break;
            }

        }
    }
}
