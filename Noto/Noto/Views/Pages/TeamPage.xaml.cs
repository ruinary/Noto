using Noto.Data;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class TeamPage : Page
    {
        public TeamPage()
        {
            InitializeComponent();
            TeamName.DataContext = DataWorker.CurrentTeam.teamName;
            DataWorker.CurrentPage.currentTaskPage = new TaskPage();
            taskFrame.DataContext = DataWorker.CurrentPage.currentTaskPage;
        }

        private void TeamSettingsButtonClick(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
