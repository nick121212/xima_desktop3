using System.ComponentModel.Composition;
using System.Windows.Controls;
using XIMALAYA.PCDesktop.Modules.UserModule;

namespace XIMALAYA.PCDesktop.Modules.SoundModule.Views
{
    /// <summary>
    /// MutiSoundView.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MutiSoundView : UserControl
    {
        public MutiSoundView()
        {
            InitializeComponent();
        }
        [Import]
        public MutiSoundViewModel ViewModel
        {
            get
            {
                return this.DataContext as MutiSoundViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
