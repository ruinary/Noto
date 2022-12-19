using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeDateDeadlineUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public ChangeDateDeadlineUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            taskDateCreationBlock.Text = DataWorker.CurrentTask.taskDeadlineDate;
            editedTaskDateCreation.Text = DataWorker.CurrentTask.taskDeadlineDate;
        }

        private void EditTaskDateCreationButtonClick(object sender, RoutedEventArgs e)
        {
            EditTaskDateCreation.Visibility = Visibility.Visible;
            CurTaskDateCreation.Visibility = Visibility.Hidden;
        }

        private void ConfTaskDateCreationButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_task_deadlinedate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                cmd.Parameters.Add("p_new_task_deadlinedate", OracleDbType.Varchar2, 30).Value = editedTaskDateCreation.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentTask.taskDeadlineDate = editedTaskDateCreation.Text.Trim();

            EditTaskDateCreation.Visibility = Visibility.Hidden;
            CurTaskDateCreation.Visibility = Visibility.Visible;

            taskDateCreationBlock.Text = DataWorker.CurrentTask.taskDeadlineDate;
            editedTaskDateCreation.Text = DataWorker.CurrentTask.taskDeadlineDate;
        }
    }
}
