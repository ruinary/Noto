using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Noto.Views.Windows
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            /* занесение данных в таблицу БД*/

            Authorization authorizationWindow = new Authorization();
            authorizationWindow.Show();
            this.Close();
        }
        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            Authorization authorizationWindow = new Authorization();
            authorizationWindow.Show();
            this.Close();
        }
    }

}
