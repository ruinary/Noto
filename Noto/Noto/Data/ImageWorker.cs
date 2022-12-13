using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using static Noto.Data.DataWorker;

namespace Noto.Data
{
    internal static class ImageWorker
    {
        public static byte[] PictureToByte(string filename) //преобразование картинки в байт по пути
        {
            byte[] pic;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                pic = new byte[fs.Length];
                fs.Read(pic, 0, pic.Length);
            }
            return pic;
        }
        public static BitmapImage LoadImage(byte[] imageData)// преобразование байтов в картинку
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public static void LoadUserImageBrush()
        {

            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmd2 = con.CreateCommand();

            cmd2.CommandText = "SELECT UserIcon FROM UserTable WHERE UPPER(UserLogin) = UPPER('" + UserProfile.userLogin + "')";
            cmd2.CommandType = CommandType.Text;

            OracleDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                UserProfile.userIconImg = LoadImage(reader.GetValue(0) as byte[]);
            }
            con.Close();

            //String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            //OracleConnection con = new OracleConnection();
            //con.ConnectionString = connectionString;

            //con.Open();
            //OracleCommand cmd = con.CreateCommand();
            //cmd.CommandText = "SELECT UserIcon FROM DBNoto.UserTable WHERE UserID = " + UserProfile.userId;;
            //cmd.CommandType = CommandType.Text;
            //OracleDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    UserProfile.userIconImg = LoadImage(reader.GetValue(0) as byte[]);
            //}
            //reader.Close();
            //con.Close();

            MessageBox.Show(UserProfile.userId + UserProfile.userLogin + UserProfile.userIconImg);
        }
        public static void UpdateUserImageBrush()
        {
            byte[] image = { };
            string imageName;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Image Files|*.jpg;*.png;"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    imageName = openFileDialog.FileName;

                    image = PictureToByte(imageName);
                    UserProfile.userIcon = image;
                }
                openFileDialog = null;
            }
            catch (ArgumentException ae)
            {
                imageName = "";
                MessageBox.Show(ae.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = connectionString;

            con.Open();

            OracleCommand cmd2 = con.CreateCommand();
            OracleTransaction txn;
            txn = con.BeginTransaction(IsolationLevel.ReadCommitted);
            cmd2.Transaction = txn;

            cmd2.CommandText = "UPDATE UserTable " +
                              "SET " +
                              "UserIcon = :ImageFront " +
                              "WHERE UPPER(UserLogin) = UPPER('" + CurrentUser.currentUserLogin + "')";

            cmd2.Parameters.Add(":ImageFront", OracleDbType.Blob);
            cmd2.Parameters[":ImageFront"].Value = image;
            cmd2.ExecuteNonQuery();

            txn.Commit();
            con.Close();

            CurrentUser.currentUserIconImg = LoadImage(image);
            UserProfile.userIconImg = LoadImage(image);
        }


        public static void LoadTeamImageBrush()
        {
            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT TeamIcon FROM DBNoto.TeamTable WHERE TeamID = " + CurrentTeam.teamId;
            cmd.CommandType = CommandType.Text;
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CurrentTeam.teamIconImg = LoadImage(reader.GetValue(0) as byte[]);
            }
            reader.Close();
            con.Close();
        }
        public static void UpdateTeamImageBrush()
        {
            byte[] image = { };
            string imageName;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Image Files|*.jpg;*.png;"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    imageName = openFileDialog.FileName;
                    image = PictureToByte(imageName);
                    CurrentTeam.teamIcon = image;
                    CurrentTeam.teamIconImg = LoadImage(image);
                }
                openFileDialog = null;
            }
            catch (ArgumentException ae)
            {
                imageName = "";
                MessageBox.Show(ae.Message.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = connectionString;

            con.Open();

            OracleCommand cmd2 = con.CreateCommand();
            OracleTransaction txn;
            txn = con.BeginTransaction(IsolationLevel.ReadCommitted);
            cmd2.Transaction = txn;

            cmd2.CommandText = "UPDATE DBNoto.TeamTable " +
                              "SET " +
                              "TeamIcon = :ImageFront " +
                              "WHERE TeamID = " + CurrentTeam.teamId;

            cmd2.Parameters.Add(":ImageFront", OracleDbType.Blob);
            cmd2.Parameters[":ImageFront"].Value = image;
            cmd2.ExecuteNonQuery();

            txn.Commit();
            con.Close();

            CurrentTeam.teamIconImg = LoadImage(image);
        }


        public static void LoadUser1ImageBrush()
        {
            String connectionString = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=system;PASSWORD=root";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = connectionString;

            con.Open();
            OracleCommand cmd2 = con.CreateCommand();

            cmd2.CommandText = "SELECT UserIcon FROM DBNoto.UserTeam_view WHERE TeamID = " + CurrentTeam.teamId + " ORDER BY UserID ASC FETCH FIRST 1 ROWS ONLY";
            cmd2.CommandType = CommandType.Text;

            OracleDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                CurrentTeam.user1IconImg = LoadImage(reader.GetValue(0) as byte[]);
            }
            reader.Close();
            con.Close();
        }
    }
}
