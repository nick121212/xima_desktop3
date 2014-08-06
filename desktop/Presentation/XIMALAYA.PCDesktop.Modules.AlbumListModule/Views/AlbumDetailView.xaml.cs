using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace XIMALAYA.PCDesktop.Modules.AlbumListModule.Views
{
    /// <summary>
    /// AlbumDetailView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AlbumDetailView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public AlbumDetailView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public AlbumDetailViewModel ViewModel
        {
            get
            {
                return this.DataContext as AlbumDetailViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
