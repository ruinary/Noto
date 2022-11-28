using Microsoft.Win32;
using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Noto.Views.Pages
{
    public partial class Profile : Page
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        byte[] image;
        string imageName;
        public Profile()
        {
            InitializeComponent();
            con.ConnectionString = connectionString;
                        
            userIconCircle.ImageSource = DataWorker.UserProfile.userIconImg;

            userEmail.Text = DataWorker.UserProfile.userEmail;
            EditedEmail.Text = DataWorker.UserProfile.userEmail;

            userPhoneNumber.Text = DataWorker.UserProfile.userPhoneNumber;
            EditedPhone.Text = DataWorker.UserProfile.userPhoneNumber;

            userName.Text = DataWorker.UserProfile.userName;
            userLastName.Text = DataWorker.UserProfile.userLastName;

            userLogin.Text = DataWorker.UserProfile.userLogin;

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId) 
            { 
                EditProfileEmail.Visibility = Visibility.Visible;
                EditProfilePhoneNumber.Visibility = Visibility.Visible;
            }
            else
            {
                EditProfileEmail.Visibility = Visibility.Hidden;
                EditProfilePhoneNumber.Visibility = Visibility.Hidden;
            }
        }

        #region Edit Email
        private void Button_Click_EditEMail(object sender, RoutedEventArgs e)
        {
            EditEmail.Visibility = Visibility.Visible;
            CurEmail.Visibility = Visibility.Hidden;
        }
        private void Button_Click_ConfEMail(object sender, RoutedEventArgs e)
        {
            bool check1 = Regex.IsMatch(EditedEmail.Text, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)");
            
            if (check1)
            {
                string curEmail = EditedEmail.Text;
                int userAuthId = DataWorker.UserProfile.userId;

                con.Open();
                OracleCommand cmd = con.CreateCommand();
                try
                {
                    cmd.CommandText = "DBNoto.update_user_email";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_user_login", OracleDbType.Varchar2, 30).Value = DataWorker.UserProfile.userLogin;
                    cmd.Parameters.Add("p_new_user_email", OracleDbType.Varchar2, 30).Value = EditedEmail.Text.Trim();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                con.Close();

                DataWorker.UserProfile.userEmail = EditedEmail.Text.Trim();

                EditEmail.Visibility = Visibility.Hidden;
                CurEmail.Visibility = Visibility.Visible;

                userEmail.Text = DataWorker.UserProfile.userEmail;
            }
            else
            {
                MessageBox.Show("Введенные данные не являются почтой!");
            }
        }
        #endregion
        #region Edit Photo
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog()
                    {
                        Filter = "Image Files|*.jpg;*.png;"
                    };

                    if (openFileDialog.ShowDialog() == true)
                    {
                        imageName = openFileDialog.FileName;
                        image = File.ReadAllBytes(openFileDialog.FileName);
                        var bitmImg = new BitmapImage();
                        using (var mem = new MemoryStream(image))
                        {
                            mem.Position = 0;
                            bitmImg.BeginInit();
                            bitmImg.CacheOption = BitmapCacheOption.OnLoad;
                            bitmImg.StreamSource = mem;
                            bitmImg.EndInit();
                        }
                        bitmImg.Freeze();
                        userIconCircle.ImageSource = bitmImg;
                        DataWorker.UserProfile.userIcon = image;
                    }
                    openFileDialog = null;
                }
                catch (System.ArgumentException ae)
                {
                    imageName = "";
                    MessageBox.Show(ae.Message.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                con.Open();
                FileStream fls;
                fls = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, Convert.ToInt32(fls.Length));
                fls.Close();

                if (imageName != "")
                {
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandText = "ALTER SESSION SET \"_ORACLE_SCRIPT\" = TRUE";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    OracleCommand cmd2 = con.CreateCommand();
                    OracleTransaction txn;
                    txn = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd2.Transaction = txn;

                    cmd2.CommandText = "UPDATE UserTable " +
                                      "SET " +
                                      "UserIcon = :ImageFront " +
                                      "WHERE UPPER(UserLogin) = UPPER('" + DataWorker.UserProfile.userLogin + "')";

                    cmd2.Parameters.Add(":ImageFront", OracleDbType.Blob);
                    cmd2.Parameters[":ImageFront"].Value = blob;

                    cmd2.ExecuteNonQuery();
                    txn.Commit();
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        #endregion
        #region Edit Phone
        private void Button_Click_EditPhone(object sender, RoutedEventArgs e)
        {
            EditPhone.Visibility = Visibility.Visible;
            CurPhone.Visibility = Visibility.Hidden;
        }

        private void Button_Click_ConfPhone(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion
    }
}
