using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class WinHeader : UserControl
    {
        public WinHeader()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Window.GetWindow(this).Close();
        }

        private void ButtonCollapse_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }
    }
}
