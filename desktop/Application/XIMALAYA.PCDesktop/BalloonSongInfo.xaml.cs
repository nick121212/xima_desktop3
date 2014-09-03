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

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// BalloonSongInfo.xaml 的交互逻辑
    /// </summary>
    public partial class BalloonSongInfo : UserControl
    {
        public BalloonSongInfo()
        {
            InitializeComponent();

            this.Loaded += BalloonSongInfo_Loaded;
            this.Unloaded += BalloonSongInfo_Unloaded;
        }

        public MainViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainViewModel;
            }
        }

        void BalloonSongInfo_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        void BalloonSongInfo_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
