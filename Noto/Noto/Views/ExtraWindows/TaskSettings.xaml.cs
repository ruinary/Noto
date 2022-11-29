using Noto.Data;
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

namespace Noto.Views.ExtraWindows
{
    public partial class TaskSettings : Window
    {
        public TaskSettings()
        {
            InitializeComponent();
            taskTitleBlock.Text = DataWorker.CurrentTask.taskTitle;
        }

        private void EditTaskTitileButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void ConfTaskTitileButtonClick(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
