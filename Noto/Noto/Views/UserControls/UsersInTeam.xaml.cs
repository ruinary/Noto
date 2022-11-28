using Noto.Data;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Noto.Views.UserControls
{
    public partial class UsersInTeam : UserControl
    {
        public UsersInTeam()
        {
            InitializeComponent();

            //BitmapImage image = new BitmapImage();
            //image.BeginInit();
            //image.StreamSource = new MemoryStream(DataWorker.UserProfile.userIcon as byte[]);
            //image.EndInit();
            //ImageBrush brush = new ImageBrush(image);
            //circleUser1.Fill = brush;
        }
    }
}
