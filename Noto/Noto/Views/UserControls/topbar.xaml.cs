using Noto.Data;
using Noto.Views.Pages;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class Topbar : UserControl
    {
        public Topbar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProfileButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.UserProfile.userId = DataWorker.CurrentUser.currentUserId;
            DataWorker.UserProfile.userLogin = DataWorker.CurrentUser.currentUserLogin;
            DataWorker.UserProfile.userEmail = DataWorker.CurrentUser.currentUserEmail;
            DataWorker.UserProfile.userPhoneNumber = DataWorker.CurrentUser.currentUserPhoneNumber;
            DataWorker.UserProfile.userName = DataWorker.CurrentUser.currentUserName;
            DataWorker.UserProfile.userLastName = DataWorker.CurrentUser.currentUserLastName;

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
