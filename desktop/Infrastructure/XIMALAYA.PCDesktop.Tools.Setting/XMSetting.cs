using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    /// <summary>
    /// 设置类
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public class XMSetting1 : IDisposable
    {
        const string DataPath = "datas/settings";
        [Export("Dictionary")]
        public PersistentDictionary<string, string> Dictionary { get; set; }
        [ImportMany]
        public IEnumerable<BaseSetting> Settings { get; set; }

        public XMSetting1()
        {
            this.Dictionary = new PersistentDictionary<string, string>(DataPath);
        }

        public void Init()
        {
            if (this.Settings != null)
            {
                foreach (var setting in this.Settings)
                {
                    setting.Init();
                }
            }
        }

        public void Save()
        {
            foreach (var setting in this.Settings)
            {
                setting.Save();
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Save();
            this.Dictionary.Dispose();
            this.Dictionary = null;
        }

        #endregion
    }

    [Export]
    public sealed class XMSetting
    {
        private static XMSetting1 _instance;

        public static XMSetting1 Instance
        {
            get
            {
                return _instance;
            }
        }
        [ImportingConstructor]
        private XMSetting(XMSetting1 instance)
        {
            _instance = instance;
            _instance.Init();
        }
    }
}
