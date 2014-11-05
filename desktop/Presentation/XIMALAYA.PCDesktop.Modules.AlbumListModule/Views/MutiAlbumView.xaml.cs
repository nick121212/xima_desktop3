using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule.Views
{
    /// <summary>
    /// MutiAlbum.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MutiAlbumView : UserControl
    {
        public MutiAlbumView()
        {
            InitializeComponent();
        }
        [Import]
        public MutiAlbumViewModel ViewModel
        {
            get
            {
                return this.DataContext as MutiAlbumViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
