using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Core.Models.Sound;
using XIMALAYA.PCDesktop.Modules.SoundListModule.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Modules.SoundListModule
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SoundDetailViewModel : BaseViewModel
    {
        #region 属性

        /// <summary>
        /// 声音数据
        /// </summary>
        public SoundData SoundData { get; set; }
        /// <summary>
        /// 播放器控件
        /// </summary>
        public BassEngine BassEngine
        {
            get
            {
                return PlayerSingleton.Instance;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 初始化入口
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="regionName"></param>
        /// <param name="soundDetailView"></param>
        public void DoInit(long trackID, string regionName, SoundDetailView soundDetailView)
        {
            if (this.RegionManager != null && this.RegionManager.Regions.ContainsRegionWithName(regionName))
            {
                this.RegionManager.AddToRegion(regionName, soundDetailView);
            }
        }
    
        #endregion
     }
}
