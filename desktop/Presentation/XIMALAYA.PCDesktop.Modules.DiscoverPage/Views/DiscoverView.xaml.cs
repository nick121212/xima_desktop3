using System.ComponentModel.Composition;
using System.Windows;

namespace XIMALAYA.PCDesktop.Modules.DiscoverPage.Views
{
    /// <summary>
    /// DiscoverView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class DiscoverView
    {
        public DiscoverView()
        {
            InitializeComponent();
            //this.Loaded += DiscoverView_Loaded;
            //this.Unloaded += DiscoverView_Unloaded;
        }

        void DiscoverView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Dispose();
            this.ViewModel = null;
        }

        [Import]
        public DiscoverViewModel ViewModel
        {
            get { return this.DataContext as DiscoverViewModel; }
            set { this.DataContext = value; }
        }

        //private void mainScroll_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    double offset = mainScroll.VerticalOffset - (e.Delta * 6 / 6);
        //    if (offset < 0)
        //    {
        //        mainScroll.ScrollToVerticalOffset(0);
        //    }
        //    else if (offset > mainScroll.ExtentHeight)
        //    {
        //        mainScroll.ScrollToVerticalOffset(mainScroll.ExtentHeight);
        //    }
        //    else
        //    {
        //        mainScroll.ScrollToVerticalOffset(offset);
        //    }

        //    e.Handled = true;
        //}
    }
}
