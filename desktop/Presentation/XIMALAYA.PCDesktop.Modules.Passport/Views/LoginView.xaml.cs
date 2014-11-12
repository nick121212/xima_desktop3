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

namespace XIMALAYA.PCDesktop.Modules.Passport.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public partial class LoginView : UserControl
    {
        /// <summary>
        /// 构造
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 页面viewmodel
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
