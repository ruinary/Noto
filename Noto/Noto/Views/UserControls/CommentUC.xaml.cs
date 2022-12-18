using Noto.Data;
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
    public partial class CommentUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";

        public CommentUC()
        {
            con.ConnectionString = connectionString;
            InitializeComponent();

            userLoginBlock.Text = DataWorker.CurrentComment.commentUserLogin;
            commentTextBlock.Text = DataWorker.CurrentComment.commentText;

            ImageWorker.LoadCommentUserImageBrush();
            userIconCircle.ImageSource = DataWorker.CurrentComment.commentUserIconImg;
        }

        public CommentUC(Int16 _idComment, string _userLogin, string _commentText)
        {
            con.ConnectionString = connectionString;
            DataWorker.CurrentComment.commentId = _idComment;
            DataWorker.CurrentComment.commentText = _commentText;
            DataWorker.CurrentComment.commentUserLogin = _userLogin;

            InitializeComponent();

            userLoginBlock.Text = DataWorker.CurrentComment.commentUserLogin;
            commentTextBlock.Text = DataWorker.CurrentComment.commentText;
            ImageWorker.LoadCommentUserImageBrush();
            userIconCircle.ImageSource = DataWorker.CurrentComment.commentUserIconImg;
        }
        void GetUserProfileInfo()
        {
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                OracleCommand cmd = con.CreateCommand();

                cmd.CommandText = "DBNoto.search_user";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = userLoginBlock.Text;

                cmd.Parameters.Add("o_user_id", OracleDbType.Int32, 10);
                cmd.Parameters["o_user_id"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_user_name", OracleDbType.Varchar2, 30);
                cmd.Parameters["o_user_name"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_user_lastname", OracleDbType.Varchar2, 30);
                cmd.Parameters["o_user_lastname"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_user_phonenumber", OracleDbType.Varchar2, 13);
                cmd.Parameters["o_user_phonenumber"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add("o_user_email", OracleDbType.Varchar2, 30);
                cmd.Parameters["o_user_email"].Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();
                int id = Convert.ToInt32((decimal)(OracleDecimal)(cmd.Parameters["o_user_id"].Value));
                string name = Convert.ToString(cmd.Parameters["o_user_name"].Value);
                string lastname = Convert.ToString(cmd.Parameters["o_user_lastname"].Value);
                string phonenumber = Convert.ToString(cmd.Parameters["o_user_phonenumber"].Value);
                string email = Convert.ToString(cmd.Parameters["o_user_email"].Value);

                DataWorker.UserProfile.userId = id;
                DataWorker.UserProfile.userLogin = userLoginBlock.Text;
                DataWorker.UserProfile.userEmail = email;
                DataWorker.UserProfile.userPhoneNumber = phonenumber;
                DataWorker.UserProfile.userName = name;
                DataWorker.UserProfile.userLastName = lastname;

                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

        }
        private void CheckProfileButtonClick(object sender, RoutedEventArgs e)
        {
            GetUserProfileInfo();

            DataWorker.CurrentPage.currentPage = new Profile();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
