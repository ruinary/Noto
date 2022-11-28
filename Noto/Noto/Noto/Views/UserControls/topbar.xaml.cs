using Noto.Views.Pages;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class Topbar : UserControl
    {
        bool flagSideBar = false;
        public Topbar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (!flagSideBar)
            {
                SideBar.Visibility = Visibility.Visible;
                flagSideBar = true;
            }
            else 
            { 
                SideBar.Visibility = Visibility.Hidden;
                flagSideBar = false;
            }
        }

        private void ProfileButtonClick(object sender, RoutedEventArgs e)
        {
            Profile profilePage = new Profile();
            MainFrame.Navigate(profilePage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TeamPage teamPage = new TeamPage();
            MainFrame.Navigate(teamPage);
        }
    }
}
