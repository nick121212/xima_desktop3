using System;
using System.ComponentModel.Composition;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule.Views
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
            this.Unloaded += AlbumView_Unloaded;
        }

        void AlbumView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ViewModel.Dispose();
            this.ViewModel = null;
        }
        /// <summary>
        /// mvvm model
        /// </summary>
        [Import]
        public AlbumViewModel ViewModel
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
