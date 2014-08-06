using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Modules.Category.Views;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.Category
{
    [Export(typeof(CategoryDetailViewModel))]
    public class CategoryDetailViewModel : NotificationObject
    {
        public IEventAggregator EventAggregator { get; set; }

        public object LastKey { get; set; }

        private void OnAlbumListEventPublish(AlbumListInfo info)
        {
            if (info.Type == AlbumListType.Category)
            {
                FlyoutVisible.Instance.IsCategoryDetailShow = true;
                MessageBox.Show("category");
            }
        }

        private bool OnAlbumListEventFilter(AlbumListInfo info)
        {
            return this.LastKey == info.Key;
        }

        [ImportingConstructor]
        public CategoryDetailViewModel(IEventAggregator eventAggregator)
        {
            AlbumListEvent albumListEvent;

            this.EventAggregator = eventAggregator;
            albumListEvent = this.EventAggregator.GetEvent<AlbumListEvent>();
            if (albumListEvent != null)
            {
                albumListEvent.Subscribe(OnAlbumListEventPublish, ThreadOption.UIThread, false, OnAlbumListEventFilter);
            }
        }
    }
}
