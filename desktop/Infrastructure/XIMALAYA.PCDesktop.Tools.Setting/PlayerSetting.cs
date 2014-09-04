using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools.Untils;

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

        public PlayerSetting()
        {
            this.IsMuted = false;
            this.Volume = 0.5F;
        }

        public override void Init()
        {

        }
    }
}
