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
