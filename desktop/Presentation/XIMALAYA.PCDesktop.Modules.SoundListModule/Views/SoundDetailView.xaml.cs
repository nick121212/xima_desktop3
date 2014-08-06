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
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.SoundListModule.Views
{
    /// <summary>
    /// SoundDetailView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SoundDetailView : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public SoundDetailView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public SoundDetailViewModel ViewModel
        {
            get
            {
                return this.DataContext as SoundDetailViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
