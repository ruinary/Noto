using Noto.Data;
using Noto.Views.Pages;
using Oracle.ManagedDataAccess.Client;
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
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        Int16 taskId;
        string taskTitle;
        string taskDeadlineDate;
        string taskCreationDate;
        string taskPriority;
        string taskStatus;
        string taskDescription;
        
        public TaskUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();
        }

        public TaskUC(Int16 _id, string _taskTitle, string _taskDeadlineDate, string _taskCreationDate, string _taskPriority, string _taskStatus, string _taskDescription)
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            this.taskId = _id;
            this.taskTitle = _taskTitle;
            this.taskDeadlineDate = _taskDeadlineDate;
            this.taskCreationDate = _taskCreationDate;
            this.taskPriority = _taskPriority;
            this.taskStatus = _taskStatus;
            this.taskDescription = _taskDescription;

            taskTitleBlock.Text = taskTitle;
            taskCreationDateBlock.Text = taskDeadlineDate;
            taskDeadlineDateBlock.Text = taskCreationDate;
            taskPriorityBlock.Text = taskPriority;
            taskStatusBlock.Text = taskStatus;
            taskDescriptionBlock.Text = taskDescription;
        }
    }
}
