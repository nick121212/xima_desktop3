using System.ComponentModel.Composition;
using System.Windows;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.MusicPlayer.Views
{
    /// <summary>
    /// MusicPlayerView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class MusicPlayerView
    {
        public MusicPlayerView()
        {
            InitializeComponent();
            this.Loaded += MusicPlayerView_Loaded;
        }

        void MusicPlayerView_Loaded(object sender, RoutedEventArgs e)
        {
            this.SpectrumAnalyzer.RegisterSoundPlayer(GlobalDataSingleton.Instance.BassEngine);
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
