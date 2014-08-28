using System.ComponentModel.Composition;

namespace XIMALAYA.PCDesktop.Modules.UserModule.Views
{
    /// <summary>
    /// UserMinView.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class UserMinView
    {
        public UserMinView()
        {
            InitializeComponent();
            this.Unloaded += UserMinView_Unloaded;
        }

        void UserMinView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ViewModel.Dispose();
            this.ViewModel = null;
        }
        [Import]
        public UserViewModel ViewModel {
            get
            {
                return this.DataContext as UserViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
