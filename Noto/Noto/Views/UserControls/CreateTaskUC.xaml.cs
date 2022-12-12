using Noto.Views.ExtraWindows;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class CreateTaskUC : UserControl
    {
        public CreateTaskUC()
        {
            InitializeComponent();
        }

        private void CreateTaskButtonClick(object sender, RoutedEventArgs e)
        {
            CreateTask createTask = new CreateTask();
            createTask.Show();
        }
    }
}
