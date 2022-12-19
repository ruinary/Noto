using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeTaskDescriptionUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public ChangeTaskDescriptionUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            taskDescription.Text = DataWorker.CurrentTask.taskDescription;
            editedTaskDescription.Text = DataWorker.CurrentTask.taskDescription;
        }

        #region Edit TaskDescription
        private void EditTaskDescriptionButtonClick(object sender, RoutedEventArgs e)
        {
            EditTaskDescriptionGrid.Visibility = Visibility.Visible;
            CurTaskDescriptionGrid.Visibility = Visibility.Hidden;
        }
        private void ConfTaskDescriptionButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_task_description";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                cmd.Parameters.Add("p_new_task_description", OracleDbType.Varchar2, 200).Value = editedTaskDescription.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentTask.taskDescription = editedTaskDescription.Text.Trim();

            EditTaskDescriptionGrid.Visibility = Visibility.Hidden;
            CurTaskDescriptionGrid.Visibility = Visibility.Visible;

            taskDescription.Text = DataWorker.CurrentTask.taskDescription;
            editedTaskDescription.Text = DataWorker.CurrentTask.taskDescription;
        }
        #endregion
    }
}
