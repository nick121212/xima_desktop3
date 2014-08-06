using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Practices.Prism.Regions;

namespace XIMALAYA.PCDesktop.Tools.RegionAdapter
{
    /// <summary>
    /// prism适配stackpanel
    /// </summary>
    [Export]
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultBehaviors"></param>
        [ImportingConstructor]
        public StackPanelRegionAdapter(IRegionBehaviorFactory defaultBehaviors)
            : base(defaultBehaviors)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="regionTarget"></param>
        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.ActiveViews.CollectionChanged += delegate
            {
                regionTarget.Children.Clear();
                foreach (var item in region.ActiveViews)
                {
                    regionTarget.Children.Add(item as UIElement);
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
