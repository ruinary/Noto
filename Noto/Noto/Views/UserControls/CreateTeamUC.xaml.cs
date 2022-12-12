using Noto.Views.ExtraWindows;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class CreateTeamUC : UserControl
    {
        public CreateTeamUC()
        {
            InitializeComponent();
        }

        private void CreateTeamButtonClick(object sender, RoutedEventArgs e)
        {
            CreateTeam createTeam = new CreateTeam();
            createTeam.Show();
        }
    }
}
