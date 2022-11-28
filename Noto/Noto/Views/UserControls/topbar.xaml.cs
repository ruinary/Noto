using Noto.Data;
using Noto.Views.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class Topbar : UserControl
    {
        public Topbar()
        {
            InitializeComponent();
            MainFrame.Content = DataWorker.CurrentPage.currentPage;
        }

        private void UserTeamsButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.CurrentPage.currentPage = new UserTeams();
            MainFrame.Content = DataWorker.CurrentPage.currentPage;
        }

        private void ProfileButtonClick(object sender, RoutedEventArgs e)
        {
            DataWorker.UserProfile.userId = DataWorker.CurrentUser.currentUserId;
            DataWorker.UserProfile.userLogin = DataWorker.CurrentUser.currentUserLogin;
            DataWorker.UserProfile.userEmail = DataWorker.CurrentUser.currentUserEmail;
            DataWorker.UserProfile.userPhoneNumber = DataWorker.CurrentUser.currentUserPhoneNumber;
            DataWorker.UserProfile.userName = DataWorker.CurrentUser.currentUserName;
            DataWorker.UserProfile.userLastName = DataWorker.CurrentUser.currentUserLastName;
            DataWorker.UserProfile.userIconImg = DataWorker.CurrentUser.currentUserIconImg;

            DataWorker.CurrentPage.currentPage = new Profile();
            MainFrame.Content = DataWorker.CurrentPage.currentPage;
        }
    }
}
