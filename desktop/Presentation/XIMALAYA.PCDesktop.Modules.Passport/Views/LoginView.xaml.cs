using System.ComponentModel.Composition;

namespace XIMALAYA.PCDesktop.Modules.Passport.Views
{
    /// <summary>
    /// NormalLoginView.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public partial class LoginView
    {
        /// <summary>
        /// 构造
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            this.Loaded += LoginView_Loaded;
        }

        void LoginView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //this.ChromeWebBrowser.init
        }
        /// <summary>
        /// viewmodel
        /// </summary>
        [Import]
        public LoginViewModel ViewModel
        {
            get
            {
                return this.DataContext as LoginViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
