using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace XIMALAYA.PCDesktop.Modules.SoundModule.Views
{
    /// <summary>
    /// SoundDetailView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SoundDetailView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public SoundDetailView()
        {
            InitializeComponent();
            this.Unloaded += SoundDetailView_Unloaded;
        }

        void SoundDetailView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Dispose();
            //this.ViewModel = null;
            //GC.Collect();
        }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public SoundDetailViewModel ViewModel
        {
            get
            {
                return this.DataContext as SoundDetailViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
