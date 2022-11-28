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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Noto.Views.Pages
{
    public partial class TeamPage : Page
    {
        public TeamPage()
        {
            InitializeComponent();
            TeamName.DataContext = DataWorker.CurrentTeam.teamName;
            Page page = new TaskPage();
            TaskFrame.Content = page;
        }

        private void TeamSettingsButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
