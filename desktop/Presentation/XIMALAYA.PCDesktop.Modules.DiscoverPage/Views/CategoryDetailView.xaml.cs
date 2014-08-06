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

namespace XIMALAYA.PCDesktop.Modules.DiscoverPage.Views
{
    /// <summary>
    /// CategoryDetailView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CategoryDetailView : UserControl
    {
        public CategoryDetailView()
        {
            InitializeComponent();
        }
        [Import]
        public CategoryDetailViewModel CategoryDetailViewModel
        {
            get { return this.DataContext as CategoryDetailViewModel; }
            set { this.DataContext = value; }
        }

    }
}
