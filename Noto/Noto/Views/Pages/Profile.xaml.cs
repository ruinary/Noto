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

            if (DataWorker.CurrentUser.currentUserId == DataWorker.UserProfile.userId)
            {
                ChangePasswordGrid.Visibility = Visibility.Visible;
            }
            else
            {
                ChangePasswordGrid.Visibility = Visibility.Hidden;
            }
        }

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
                                      "WHERE UPPER(UserLogin) = UPPER('" + DataWorker.CurrentUser.currentUserLogin + "')";

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
    }
}
