using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class CalendarPage : Page
    {
        OracleConnection con = new OracleConnection();
         
        int rowMargin = 2, rowCounter = 1;
        string date = DateTime.Now.ToString("dd.MM.yyyy");
        public CalendarPage()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();

            loadSomeTasks();
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            date = datePicker.SelectedDate.Value.ToString("dd.MM.yyyy");

            loadSomeTasks();
        }
        private void showBackPageTasksButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter -= rowMargin;
            rowCounter--;
            loadSomeTasks();
        }

        private void showNextPageTasksButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter += rowMargin;
            rowCounter++;
            loadSomeTasks();
        }

        public void loadSomeTasks()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select TaskID, TaskTitle, CreationDate, DeadlineDate, TaskPriorityName, TaskStatusName, TaskDescription, row_number() over (ORDER BY TaskID ASC) rn from DBNoto.TaskTeam_view WHERE TeamID = " + DataWorker.CurrentTeam.teamId + " AND DeadlineDate = '" + date + "') where rn between :n and :m ORDER BY TaskID ASC";
            cmd.Parameters.Add(new OracleParameter("n", rowCounter));
            cmd.Parameters.Add(new OracleParameter("m", rowCounter + rowMargin));
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            taskList.Children.Clear();
            while (reader.Read())
            {
                TaskUC task = new TaskUC(reader.GetInt16(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                taskList.Children.Add(task);
            }
            con.Close();
        }
    }
}
