using Noto.Views.ExtraWindows;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.UserControls
{
    public partial class CreateCommentUC : UserControl
    {
        public CreateCommentUC()
        {
            InitializeComponent();
        }

        private void CreateCommentButtonClick(object sender, RoutedEventArgs e)
        {
            CreateComment createComment = new CreateComment();
            createComment.Show();
        }
    }
}
