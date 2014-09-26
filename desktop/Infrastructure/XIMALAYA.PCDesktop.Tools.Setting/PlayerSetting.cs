using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools.Untils;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    public class PlayerSetting : BaseSetting
    {
        /// <summary>
        /// 当前音量
        /// </summary>
        public float Volume
        {
            get
            {
                return PlayerSingleton.Instance.Volume;
            }
            set
            {
                PlayerSingleton.Instance.Volume = value;
            }
        }
        /// <summary>
        /// 是否静音
        /// </summary>
        public bool IsMuted
        {
            get
            {
                return PlayerSingleton.Instance.IsMuted;
            }
            set
            {
                PlayerSingleton.Instance.IsMuted = value;
            }
        }
        /// <summary>
        /// 播放模式
        /// </summary>
        public bool? PlayMode
        {
            get
            {
                return GlobalDataSingleton.Instance.PlayMode;
            }
            set
            {
                GlobalDataSingleton.Instance.PlayMode = value;
            }
        }

        public PlayerSetting()
        {
            this.IsMuted = false;
            this.Volume = 0.5F;
        }

        public override void Init()
        {
            Type type = typeof(PlayerSetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    var Player = XmlUtil.Deserialize(type, val) as PlayerSetting;
                    this.SetData(this, Player);
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }
        }

        public override void Save()
        {
            Type type = typeof(PlayerSetting);

            try
            {
                string val = XmlUtil.Serializer(type, this);

                this.Dictionary[type.Name] = val;
            }
            catch { }
        }
    }
}
