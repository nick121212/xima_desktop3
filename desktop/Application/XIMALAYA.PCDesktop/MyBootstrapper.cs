using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using XIMALAYA.PCDesktop.Tools.Behaviors;
using XIMALAYA.PCDesktop.Tools.RegionAdapter;
using System.IO;
using System.Reflection;
using XIMALAYA.PCDesktop.Tools.Themes;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Events;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// 启动初始化类
    /// </summary>
    //[CLSCompliant(false)]
    public class MyBootstrapper : MefBootstrapper
    {
        private readonly CallbackLogger callbackLogger = new CallbackLogger();
        private IRegionManager regionmanager { get; set; }
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="DependencyObject"/>, the
        /// <see cref="MefBootstrapper"/> will attach the default <seealso cref="Microsoft.Practices.Composite.Regions.IRegionManager"/> of
        /// the application in its <see cref="Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref=""Microsoft.Practices.Composite.Presentation.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implemention ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings regionAdapterMappings = base.ConfigureRegionAdapterMappings();

            //this.regionmanager = this.Container.GetExportedValue<IRegionManager>();
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), this.Container.GetExportedValue<StackPanelRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(Flyout), this.Container.GetExportedValue<FlyoutRegionAdapter>());

            return regionAdapterMappings;
        }

        //protected override CompositionContainer CreateContainer()
        //{
        //    var container = base.CreateContainer();

        //    container.ComposeExportedValue<IEventAggregator>(new EventAggregator());

        //    return container;
        //}

        /// <summary>
        /// Configures the <see cref="AggregateCatalog"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add this assembly to export ModuleTracker

            //this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(Directory.GetCurrentDirectory()));
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            base.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));

            //this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog("./Microsoft.Practices.Prism.MefExtensions.dll"));
            base.AggregateCatalog.Catalogs.Add(new AssemblyCatalog("./XIMALAYA.PCDesktop.Tools.dll"));
            base.AggregateCatalog.Catalogs.Add(new AssemblyCatalog("./XIMALAYA.PCDesktop.Core.Data.dll"));
            base.AggregateCatalog.Catalogs.Add(new AssemblyCatalog("./XIMALAYA.PCDesktop.Core.Services.dll"));
            base.AggregateCatalog.Catalogs.Add(new AssemblyCatalog("./XIMALAYA.PCDesktop.Core.ParamsModel.dll"));

            //this.Container.ComposeParts(this.AggregateCatalog.)
            // These modules are not referenced in the project and are discovered by inspecting a directory.
            // Both projects have a post-build step to copy themselves into that directory.
            //DirectoryCatalog catalog = new DirectoryCatalog("./modules");
            //this.AggregateCatalog.Catalogs.Add(catalog);
        }

        /// <summary>
        /// Configures the <see cref="CompositionContainer"/>.
        /// May be overwritten in a derived class to add specific type mappings required by the application.
        /// </summary>
        /// <remarks>
        /// The base implementation registers all the types direct instantiated by the bootstrapper with the container.
        /// The base implementation also sets the ServiceLocator provider singleton.
        /// </remarks>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Because we created the CallbackLogger and it needs to be used immediately, we compose it to satisfy any imports it has.
            this.Container.ComposeExportedValue<CallbackLogger>(this.callbackLogger);
        }

        /// <summary>
        /// Creates the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        /// <returns>
        /// A ConfigurationModuleCatalog.
        /// </returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            // When using MEF, the existing Prism ModuleCatalog is still the place to configure modules via configuration files.
            return new ConfigurationModuleCatalog();
        }

        /// <summary>
        /// Create the <see cref="ILoggerFacade"/> used by the bootstrapper.
        /// </summary>
        /// <remarks>
        /// The base implementation returns a new TextLogger.
        /// </remarks>
        /// <returns>
        /// A CallbackLogger.
        /// </returns>
        protected override ILoggerFacade CreateLogger()
        {
            // Because the Shell is displayed after most of the interesting boostrapper work has been performed,
            // this quickstart uses a special logger class to hold on to early log entries and display them 
            // after the UI is visible.

            return this.callbackLogger;
        }
    }
}
