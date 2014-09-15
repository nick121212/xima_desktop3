using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace XIMALAYA.PCDesktop.Modules.Menus.Views
{
    /// <summary>
    /// MainMenuView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        [Import]
        public MainMenuViewModel ViewModel
        {
            get { return this.DataContext as MainMenuViewModel; }
            set { this.DataContext = value; }
        }
    }
}
