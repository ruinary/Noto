using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangePriorityUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public ChangePriorityUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            switch (DataWorker.CurrentTask.taskPriority)
            {
                case "HIGH":
                    selectorHigh.IsSelected = true;
                    break;
                case "MEDIUM":
                    selectorMedium.IsSelected = true;
                    break;
                case "LOW":
                    selectorLow.IsSelected = true;
                    break;
            }
        }
        private void Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            OracleCommand cmd = con.CreateCommand();
            switch ((prioritySelection.SelectedItem as ListBoxItem).Content.ToString())
            {
                case "HIGH":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_priority";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_priority", OracleDbType.Int16, 10).Value = 1;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskPriority = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                    }
                    break;
                case "MEDIUM":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_priority";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_priority", OracleDbType.Int16, 10).Value = 2;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskPriority = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                    }
                    break;
                case "LOW":
                    try
                    {
                        con.Open();
                        cmd.CommandText = "DBNoto.update_task_priority";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                        cmd.Parameters.Add("p_new_task_priority", OracleDbType.Int16, 10).Value = 3;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DataWorker.CurrentTask.taskPriority = (prioritySelection.SelectedItem as ListBoxItem).Content.ToString();
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
