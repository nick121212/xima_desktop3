using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Modules.Category.Views;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.Category
{
    /// <summary>
    /// 分类详情模块
    /// </summary>
    [ModuleExport(WellKnownModuleNames.CategoryModule, typeof(CategoryModule))]
    public class CategoryModule : IModule
    {
        private IRegionManager RegionManager { get; set; }
        private IServiceLocator Container { get; set; }

        public void Initialize()
        {
            if (this.RegionManager.Regions.ContainsRegionWithName(WellKnownRegionNames.CategoryDetailViewRegion))
            {
                var view = this.Container.GetInstance<CategoryDetailView>();
                var region = this.RegionManager.Regions[WellKnownRegionNames.CategoryDetailViewRegion];

                region.Add(view, WellKnownModuleNames.CategoryModule);
            }
        }
        [ImportingConstructor]
        public CategoryModule(IRegionManager regionManager, IServiceLocator container)
        {
            this.Container = container;
            this.RegionManager = regionManager;
        }
    }
}
