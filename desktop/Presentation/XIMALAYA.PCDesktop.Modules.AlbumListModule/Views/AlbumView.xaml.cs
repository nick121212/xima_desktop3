using System.ComponentModel.Composition;

namespace XIMALAYA.PCDesktop.Modules.AlbumListModule.Views
{
    /// <summary>
    /// AlbumListView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AlbumView
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AlbumView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// mvvm model
        /// </summary>
        [Import]
        public AlbumViewModel AlbumViewModel
        {
            set
            {
                this.DataContext = value;
            }
            get
            {
                return this.DataContext as AlbumViewModel;
            }
        }
    }
}
