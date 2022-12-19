using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeDateCreationUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public ChangeDateCreationUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            taskDateCreationBlock.Text = DataWorker.CurrentTask.taskCreationDate;
            editedTaskDateCreation.Text = DataWorker.CurrentTask.taskCreationDate;
        }

        private void EditTaskDateCreationButtonClick(object sender, RoutedEventArgs e)
        {
            EditTaskDateCreation.Visibility = Visibility.Visible;
            CurTaskDateCreation.Visibility = Visibility.Hidden;
        }

        private void ConfTaskDateCreationButtonClick(object sender, RoutedEventArgs e)
        {
            string curTaskDateCreation = editedTaskDateCreation.Text;
            int userAuthId = DataWorker.UserProfile.userId;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_task_creationdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                cmd.Parameters.Add("p_new_task_creationdate", OracleDbType.Varchar2, 30).Value = editedTaskDateCreation.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentTask.taskCreationDate = editedTaskDateCreation.Text.Trim();

            EditTaskDateCreation.Visibility = Visibility.Hidden;
            CurTaskDateCreation.Visibility = Visibility.Visible;

            taskDateCreationBlock.Text = DataWorker.CurrentTask.taskCreationDate;
            editedTaskDateCreation.Text = DataWorker.CurrentTask.taskCreationDate;
        }
    }
}
