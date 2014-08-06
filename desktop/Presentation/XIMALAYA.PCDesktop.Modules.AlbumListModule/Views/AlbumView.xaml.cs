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
