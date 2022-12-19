using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls.CreateUC
{
    public partial class CreateCommentUC : UserControl
    {
        OracleConnection con = new OracleConnection();
         
        public CreateCommentUC()
        {
            InitializeComponent();
        }

        private void ConfCommentTitleButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
                InitializeComponent();

                con.Open();

                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.create_comment";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_user_id", OracleDbType.Varchar2, 30).Value = DataWorker.CurrentUser.currentUserId;
                cmd.Parameters.Add("p_task_id", OracleDbType.Varchar2, 30).Value = DataWorker.CurrentTask.taskId;
                cmd.Parameters.Add("p_comment_text", OracleDbType.Varchar2, 30).Value = createCommentText.Text.Trim();

                cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
