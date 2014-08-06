using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Threading;
using XIMALAYA.PCDesktop.Tools.Player;

namespace XIMALAYA.PCDesktop.Modules.MusicPlayer.Views
{
    /// <summary>
    /// MusicPlayerView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class MusicPlayerView1
    {
        public MusicPlayerView1()
        {
            InitializeComponent();
            this.Loaded += MusicPlayerView_Loaded;
        }

        void MusicPlayerView_Loaded(object sender, RoutedEventArgs e)
        {
            //this.SpectrumAnalyzer.RegisterSoundPlayer(this.ViewModel.BassEngine);
        }

        [Import]
        public MusicPlayerViewModel ViewModel
        {
            get
            {
                return this.DataContext as MusicPlayerViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
