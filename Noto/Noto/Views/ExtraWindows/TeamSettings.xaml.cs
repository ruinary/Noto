using Microsoft.Win32;
using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Views.ExtraWindows
{
    public partial class TeamSettings : Window
    {
        OracleConnection con = new OracleConnection();
        String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
        byte[] image;
        string imageName;
        int rowMargin = 5, rowCounter = 0;

        public TeamSettings()
        {
            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmd0 = con.CreateCommand();
            cmd0.CommandText = "ALTER SESSION SET \"_ORACLE_SCRIPT\" = TRUE";
            cmd0.CommandType = CommandType.Text;
            cmd0.ExecuteNonQuery();
            con.Close();

            InitializeComponent();

            //con.Open();
            //OracleCommand cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT * FROM DBNoto.UserTeam_view WHERE TeamID = " + DataWorker.CurrentTeam.teamId + " ORDER BY UserLogin ASC";
            //cmd.CommandType = CommandType.Text;
            //OracleDataReader reader = cmd.ExecuteReader();
            //userList.Children.Clear();
            //while (reader.Read())
            //{
            //    UserUC user = new UserUC(reader.GetInt16(0), reader.GetString(1), reader.GetInt16(3), reader.GetString(4), reader.GetString(5));
            //    userList.Children.Add(user);
            //}
            //con.Close();

            loadSomeUsers();
        }

        public void loadSomeUsers()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select UserID, UserLogin, UserTeamPrivName, row_number() over (order by UserLogin) rn from DBNoto.UserTeam_view WHERE TeamID = '"+DataWorker.CurrentTeam.teamId +"') where rn between :n and :m ORDER BY UserLogin ASC";
            cmd.Parameters.Add(new OracleParameter("n", rowCounter));
            cmd.Parameters.Add(new OracleParameter("m", rowCounter + rowMargin));

            rowCounter += rowMargin;
            rowCounter++;

            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            userList.Children.Clear();
            while (reader.Read())
            {
                UserUC user = new UserUC(reader.GetInt16(0), reader.GetString(1), reader.GetString(2));
                userList.Children.Add(user);
            }
            con.Close();
        }

        private void showNextPageUsersButtonClick(object sender, RoutedEventArgs e)
        {
            loadSomeUsers();
        }

        private void DeleteTeamButtonClick(object sender, RoutedEventArgs e)
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "DBNoto.delete_team";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Команда успешно удалена");

                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            con.Close();
        }

        private void ChangeTeamIconButtonClick(object sender, RoutedEventArgs e)
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
                    teamIconCircle.ImageSource = bitmImg;
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
            try
            {
                OracleCommand cmd = con.CreateCommand();
                con.Open();
                // запись изображения в таблицу
                FileStream fls;
                fls = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();

                if (imageName != "")
                {
                    OracleCommand cmd2 = con.CreateCommand();
                    OracleTransaction txn;
                    txn = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd2.Transaction = txn;

                    cmd2.CommandText = "UPDATE TeamTable " +
                                      "SET " +
                                      "TeamIcon = :ImageFront " +
                                      "WHERE TeamID = " + DataWorker.CurrentTeam.teamId + " )";

                    cmd2.Parameters.Add(":ImageFront", OracleDbType.Blob);
                    cmd2.Parameters[":ImageFront"].Value = blob;

                    cmd2.ExecuteNonQuery();
                    txn.Commit();
                    con.Close();
                    con.Dispose();

                    MessageBox.Show("Данные добавлены в поле blob из " + imageName);

                    this.Close();
                    Application.Current.Windows[0].Show();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при добавлении (ты лох тупой)");
            }
        }
    }
}
