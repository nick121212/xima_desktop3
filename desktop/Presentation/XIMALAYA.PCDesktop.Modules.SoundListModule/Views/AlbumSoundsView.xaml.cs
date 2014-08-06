using System.ComponentModel.Composition;

namespace XIMALAYA.PCDesktop.Modules.SoundListModule.Views
{
    /// <summary>
    /// SoundListView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AlbumSoundsView
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AlbumSoundsView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// mvvm model
        /// </summary>
        [Import]
        public AlbumSoundsViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
            get
            {
                return this.DataContext as AlbumSoundsViewModel;
            }
        }
    }
}
