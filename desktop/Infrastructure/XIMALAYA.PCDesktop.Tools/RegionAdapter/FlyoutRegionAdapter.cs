using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Regions;

namespace XIMALAYA.PCDesktop.Tools.RegionAdapter
{
    /// <summary>
    /// prism 适配器
    /// </summary>
    [Export]
    public class FlyoutRegionAdapter : RegionAdapterBase<Flyout>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultBehaviors"></param>
        [ImportingConstructor]
        public FlyoutRegionAdapter(IRegionBehaviorFactory defaultBehaviors)
            : base(defaultBehaviors)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="regionTarget"></param>
        protected override void Adapt(IRegion region, Flyout regionTarget)
        {
            region.ActiveViews.CollectionChanged += delegate
            {
                regionTarget.Content = null;

                //regionTarget.Content = region.ActiveViews[0];

                foreach (var item in region.ActiveViews)
                {
                    regionTarget.Content = item as UIElement;
                    break;
                    //regionTarget.Children.Add(item as UIElement);
                }
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
