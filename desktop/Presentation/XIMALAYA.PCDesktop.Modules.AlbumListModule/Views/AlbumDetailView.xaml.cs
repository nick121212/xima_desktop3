using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace XIMALAYA.PCDesktop.Modules.AlbumModule.Views
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
            this.Unloaded += AlbumDetailView_Unloaded;
        }

        void AlbumDetailView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ViewModel.Dispose();
            this.ViewModel = null;
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
