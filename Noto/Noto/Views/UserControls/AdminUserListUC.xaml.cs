using Noto.Data;
using Noto.Views.UserControls;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class AdminUserListUC : UserControl
    {
        OracleConnection con = new OracleConnection();
        int rowMargin = 4, rowCounter = 1;

        public AdminUserListUC()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();
            loadSomeUsers();
        }

        #region users
        public void loadSomeUsers()
        {
            con.Open();
            OracleCommand cmd = con.CreateCommand();

            cmd.CommandText = "SELECT * FROM (select UserID, UserLogin, row_number() over (order by UserLogin) rn from DBNoto.UserTable) where rn between :n and :m ORDER BY UserLogin ASC";
            cmd.Parameters.Add(new OracleParameter("n", rowCounter));
            cmd.Parameters.Add(new OracleParameter("m", rowCounter + rowMargin));

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
            rowCounter += rowMargin;
            rowCounter++;
            loadSomeUsers();
        }

        private void exportUsersButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.users_export";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось экспортировать данные!");
            }
        }

        private void importUsersButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = "DBNoto.users_import";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось импортировать данные!");
            }
        }

        private void showBackPageUsersButtonClick(object sender, RoutedEventArgs e)
        {
            rowCounter -= rowMargin;
            rowCounter--;
            loadSomeUsers();
        }

        #endregion
    }
}
