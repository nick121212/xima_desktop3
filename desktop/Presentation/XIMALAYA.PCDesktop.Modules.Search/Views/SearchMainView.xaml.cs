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

namespace XIMALAYA.PCDesktop.Modules.Search.Views
{
    /// <summary>
    /// SearchMainView.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SearchMainView : UserControl
    {
        public SearchMainView()
        {
            InitializeComponent();
        }

        [Import]
        public SearchMainViewModel ViewModel
        {
            get { return this.DataContext as SearchMainViewModel; }
            set { this.DataContext = value; }
        }
    }
}
