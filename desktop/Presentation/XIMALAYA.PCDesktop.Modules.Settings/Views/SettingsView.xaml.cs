using System.ComponentModel.Composition;

namespace XIMALAYA.PCDesktop.Modules.Settings.Views
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        [Import]
        public SettingsViewModel ViewModel
        {
            get { return this.DataContext as SettingsViewModel; }
            set { this.DataContext = value; }
        }
    }
}
