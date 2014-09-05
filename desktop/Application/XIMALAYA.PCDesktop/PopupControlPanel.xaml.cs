using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// PopupControlPanel.xaml 的交互逻辑
    /// </summary>
    //[Export,PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PopupControlPanel : UserControl
    {
        public PopupControlPanel()
        {
            InitializeComponent();
            TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }

        private void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

    }
}
