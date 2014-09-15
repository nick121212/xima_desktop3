using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    /// <summary>
    /// 设置类
    /// </summary>
    public class XMSetting : Singleton<XMSetting>, IDisposable
    {
        const string DataPath = "datas/settings";
        private PersistentDictionary<string, string> Dictionary { get; set; }
        /// <summary>
        /// 播放器相关设置
        /// </summary>
        public PlayerSetting Player { get; private set; }
        /// <summary>
        /// 热键相关设置
        /// </summary>
        public HotKeySetting HotKey { get; private set; }
        /// <summary>
        /// 外观相关设置
        /// </summary>
        public AppearanceSetting Appearance { get; private set; }

        private XMSetting()
        {
            this.Dictionary = new PersistentDictionary<string, string>(DataPath);

            this.InitPlayerSetting();
            this.InitHotKeySetting();
            this.InitAppearanceSetting();
        }
        ~XMSetting()
        {
            this.Dispose();
        }
        private void InitHotKeySetting()
        {
            Type type = typeof(HotKeySetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    this.HotKey = XmlUtil.Deserialize(type, val) as HotKeySetting;
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }

            if (this.HotKey == null)
            {
                this.HotKey = new HotKeySetting();
            }
            this.HotKey.Init();
        }
        private void InitAppearanceSetting()
        {
            Type type = typeof(AppearanceSetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    this.Appearance = XmlUtil.Deserialize(type, val) as AppearanceSetting;
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }

            if (this.Appearance == null)
            {
                this.Appearance = new AppearanceSetting();
            }
            this.Appearance.Init();
        }
        private void InitPlayerSetting()
        {
            Type type = typeof(PlayerSetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    this.Player = XmlUtil.Deserialize(type, val) as PlayerSetting;
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }

            if (this.Player == null)
            {
                this.Player = new PlayerSetting();
            }
            this.Player.Init();
        }
        private void SavePlayerSetting()
        {
            Type type = typeof(PlayerSetting);

            try
            {
                string val = XmlUtil.Serializer(type, this.Player);

                this.Dictionary[type.Name] = val;
            }
            catch { }

        }
        private void SaveAppearanceSetting()
        {
            Type type = typeof(AppearanceSetting);

            try
            {
                string val = XmlUtil.Serializer(type, this.Appearance);

                this.Dictionary[type.Name] = val;
            }
            catch { }
        }
        private void SaveHotKeySetting()
        {
            Type type = typeof(HotKeySetting);

            try
            {
                string val = XmlUtil.Serializer(type, this.HotKey);

                this.Dictionary[type.Name] = val;
            }
            catch { }
        }
        public void Save()
        {
            this.SaveHotKeySetting();
            this.SaveAppearanceSetting();
            this.SavePlayerSetting();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.Save();
            this.Dictionary = null;
            this.Player = null;
            this.HotKey = null;
            this.Appearance = null;
        }

        #endregion
    }
}
