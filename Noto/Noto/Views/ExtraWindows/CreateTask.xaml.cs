using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.ExtraWindows
{
    public partial class CreateTask : Window
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root"; 
        bool flagTitle = false;
        bool flagDescription = false;
        bool flagDataDeadline = false;
        bool flagPrioiry = false;
        bool flagStatus =  false;
        int statusId = 0;
        int priprityId = 0;
        public CreateTask()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();
            taskDaataCreationBlock.Text = DateTime.Today.ToString("d");
        }

        private void Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            flagPrioiry = true;
            switch ((prioritySelection.SelectedItem as ListBoxItem).Content.ToString())
            {
                case "HIGH":
                    statusId = 1;
                    break;
                case "MEDIUM":
                    statusId = 2;
                    break;
                case "LOW":
                    statusId = 3;
                    break;
            }
        }
        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            flagStatus = true;
            switch ((statusSelection.SelectedItem as ListBoxItem).Content.ToString())
            {
                case "NOT STARTED":
                    priprityId = 1;
                    break;
                case "STARTED":
                    priprityId = 2;
                    break;
                case "IN PROCESS":
                    priprityId = 3;
                    break;
                case "READY":
                    priprityId = 4;
                    break;
            }
        }

        private void ConfCreateTaskButtonClick(object sender, RoutedEventArgs e)
        {
            if (taskTitleBlock.Text != null) flagTitle = true; else flagTitle = false;
            if (taskTitleBlock.Text != null) flagDescription = true; else flagDescription = false;
            if (taskDataDeadlineBlock.Text != null) flagDataDeadline = true; else flagDataDeadline = false;


            if (flagTitle && flagDescription && flagDataDeadline && flagStatus && flagPrioiry)
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                try
                {
                    cmd.CommandText = "DBNoto.create_task";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.CurrentUser.currentUserLogin;
                    cmd.Parameters.Add("p_team_name", OracleDbType.Varchar2, 13).Value = DataWorker.CurrentTeam.teamName;
                    cmd.Parameters.Add("p_task_title", OracleDbType.Varchar2, 30).Value = taskTitleBlock.Text;
                    cmd.Parameters.Add("p_task_status", OracleDbType.Int32, 10).Value = statusId;
                    cmd.Parameters.Add("p_task_priority", OracleDbType.Int32, 10).Value = priprityId;
                    cmd.Parameters.Add("p_task_description", OracleDbType.Varchar2, 200).Value = taskDescriptionBlock.Text;
                    cmd.Parameters.Add("p_task_deadlineDate", OracleDbType.Varchar2, 30).Value = taskDataDeadlineBlock.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                con.Close();
            }
            else 
            {
                if (flagTitle == false) { MessageBox.Show("Укажите заголовок для задания!"); }
                if (flagDescription == false) { MessageBox.Show("Укажите описание для задания!"); }
                if (flagDataDeadline == false) { MessageBox.Show("Укажите дату завершения для задания!"); }
                if (flagPrioiry == false) { MessageBox.Show("Укажите приоритет для задания!"); }
                if (flagStatus == false) { MessageBox.Show("Укажите статус для задания!"); }
            }


        }
    }
}
