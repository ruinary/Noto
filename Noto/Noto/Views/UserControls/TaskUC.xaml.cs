using Noto.Data;
using Noto.Views.ExtraWindows;
using Noto.Views.Pages;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Views.UserControls
{
    public partial class TaskUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        Int16 taskId;
        string taskTitle;
        string taskDeadlineDate;
        string taskCreationDate;
        string taskPriority;
        string taskStatus;
        string taskDescription;
        
        public TaskUC()
        {
            InitializeComponent();
        }

        public TaskUC(Int16 _id, string _taskTitle,  string _taskCreationDate, string _taskDeadlineDate, string _taskPriority, string _taskStatus, string _taskDescription)
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;

            InitializeComponent();

            this.taskId = _id;
            this.taskTitle = _taskTitle;
            this.taskDeadlineDate = _taskDeadlineDate;
            this.taskCreationDate = _taskCreationDate;
            this.taskPriority = _taskPriority;
            this.taskStatus = _taskStatus;
            this.taskDescription = _taskDescription;

            taskTitleBlock.Text = taskTitle;
            taskCreationDateBlock.Text = taskCreationDate;
            taskDeadlineDateBlock.Text = taskDeadlineDate;
            taskPriorityBlock.Text = taskPriority;
            taskStatusBlock.Text = taskStatus;
            taskDescriptionBlock.Text = taskDescription;

            DataWorker.CurrentTask.taskId = taskId;
            DataWorker.CurrentTask.taskTitle = taskTitle;
            DataWorker.CurrentTask.taskDeadlineDate = taskDeadlineDate;
            DataWorker.CurrentTask.taskCreationDate = taskCreationDate;
            DataWorker.CurrentTask.taskPriority = taskPriority;
            DataWorker.CurrentTask.taskStatus = taskStatus;
            DataWorker.CurrentTask.taskDescription = taskDescription;

            GetLastComment();

        }
        void GetLastComment()
        {

            try
            {
                con.Open();

                OracleCommand cmd = con.CreateCommand();

                cmd.CommandText = "DBNoto.get_last_comment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_task_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;

                cmd.Parameters.Add("o_user_id", OracleDbType.Int32, 10);
                cmd.Parameters["o_user_id"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_user_login", OracleDbType.Varchar2, 30);
                cmd.Parameters["o_user_login"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_comment_date", OracleDbType.Varchar2, 30);
                cmd.Parameters["o_comment_date"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_comment_text", OracleDbType.Varchar2, 100);
                cmd.Parameters["o_comment_text"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                //string comDate = Convert.ToString(cmd.Parameters["o_comment_date"].Value); 
                DataWorker.CurrentComment.commentUserId = Convert.ToInt32((decimal)(OracleDecimal)(cmd.Parameters["o_user_id"].Value));
                DataWorker.CurrentComment.commentTaskId = DataWorker.CurrentTask.taskId;
                DataWorker.CurrentComment.commentUserLogin = Convert.ToString(cmd.Parameters["o_user_login"].Value);
                DataWorker.CurrentComment.commentText = Convert.ToString(cmd.Parameters["o_comment_text"].Value);

                //MessageBox.Show(DataWorker.CurrentComment.commentUserId + DataWorker.CurrentComment.commentTaskId+ DataWorker.CurrentComment.commentUserLogin + DataWorker.CurrentComment.commentText);
                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataWorker.CurrentTask.taskId = taskId;
            DataWorker.CurrentTask.taskTitle = taskTitle;
            DataWorker.CurrentTask.taskDeadlineDate = taskDeadlineDate;
            DataWorker.CurrentTask.taskCreationDate = taskCreationDate;
            DataWorker.CurrentTask.taskPriority = taskPriority;
            DataWorker.CurrentTask.taskStatus = taskStatus;
            DataWorker.CurrentTask.taskDescription = taskDescription;

            TaskSettings taskSettings = new TaskSettings();
            taskSettings.Show();
        }
    }
}
