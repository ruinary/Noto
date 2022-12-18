using Noto.Data;
using Noto.Views.Pages;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;

namespace Noto.Views.ExtraWindows
{
    public partial class TaskSettings : Window
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        int rowMargin = 2, rowCounter = 1;
        public TaskSettings()
        {
            con.ConnectionString = connectionString;

            InitializeComponent();
            loadSomeComments();
        }
        #region Comments
        private void showBackPageTasksButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter -= rowMargin;
            rowCounter--;
            loadSomeComments();
            CreateCommentUC newcomment = new CreateCommentUC();
            commentList.Children.Add(newcomment);
        }

        private void showNextPageTasksButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter += rowMargin;
            rowCounter++;
            loadSomeComments();
            CreateCommentUC newcomment = new CreateCommentUC();
            commentList.Children.Add(newcomment);
        }

        public void loadSomeComments()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select ComID, UserLogin, ComText,ComDate, row_number() over (ORDER BY TO_DATE(ComDate, 'DD.MM.YYYY') DESC) rn from DBNoto.UserComment_view WHERE ComTask = " + DataWorker.CurrentTask.taskId + ") where rn between :n and :m";
            //SELECT * FROM (select ComID, UserLogin, ComText,ComDate, row_number() over (ORDER BY TO_DATE(ComDate, 'DD.MM.YYYY') DESC) rn from DBNoto.UserComment_view WHERE ComTask = " + DataWorker.CurrentTask.taskId + ") where rn between :n and :m;
            cmd.Parameters.Add(new OracleParameter("n", rowCounter));
            cmd.Parameters.Add(new OracleParameter("m", rowCounter + rowMargin));
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            commentList.Children.Clear();
            while (reader.Read())
            {
                CommentUC comment = new CommentUC(reader.GetInt16(0), reader.GetString(1), reader.GetString(2));
                commentList.Children.Add(comment);
            }
            con.Close();
        }
        #endregion
        #region Delete Task
        private void DeleteTaskButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.delete_task";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTask.taskId;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Задание успешно удалено");

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            con.Close();
        }
        #endregion
    }
}
