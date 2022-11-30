using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.ChangeUC
{
    public partial class ChangeTaskTitleUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        public ChangeTaskTitleUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            taskTitleBlock.Text = DataWorker.CurrentTask.taskTitle;
            editedTaskTitle.Text = DataWorker.CurrentTask.taskTitle;
        }

        private void EditTaskTitleButtonClick(object sender, RoutedEventArgs e)
        {
            EditTaskTitle.Visibility = Visibility.Visible;
            CurTaskTitle.Visibility = Visibility.Hidden;
        }

        private void ConfTaskTitleButtonClick(object sender, RoutedEventArgs e)
        {
            string curTaskTitle = editedTaskTitle.Text;
            int userAuthId = DataWorker.UserProfile.userId;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = "DBNoto.update_task_title";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;
                cmd.Parameters.Add("p_new_task_title", OracleDbType.Varchar2, 30).Value = editedTaskTitle.Text.Trim();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            con.Close();

            DataWorker.CurrentTask.taskTitle = editedTaskTitle.Text.Trim();

            EditTaskTitle.Visibility = Visibility.Hidden;
            CurTaskTitle.Visibility = Visibility.Visible;

            taskTitleBlock.Text = DataWorker.CurrentTask.taskTitle;
        }
    }
}
