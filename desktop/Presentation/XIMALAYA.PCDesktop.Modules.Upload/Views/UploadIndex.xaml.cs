using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace XIMALAYA.PCDesktop.Modules.Upload.Views
{
    /// <summary>
    /// UploadIndex.xaml 的交互逻辑
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public partial class UploadIndex
    {
        public UploadIndex()
        {
            InitializeComponent();
        }
    }
}
