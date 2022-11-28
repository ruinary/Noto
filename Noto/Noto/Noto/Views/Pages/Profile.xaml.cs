using Noto.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Noto.Views.Pages
{
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            
            Uri imageUri = new Uri(DataWorker.UserProfile.userIcon, UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            userIconCircle.ImageSource = imageBitmap;

            userEmail.Text = DataWorker.UserProfile.userEmail;

            if (DataWorker.CurrentUserId == DataWorker.UserProfile.userId) 
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

        }
        #endregion
        #region Edit Photo
        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
