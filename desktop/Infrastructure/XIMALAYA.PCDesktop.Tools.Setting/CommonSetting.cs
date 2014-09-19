using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    public class CommonSetting : BaseSetting
    {
        /// <summary>
        /// 开机自动启动
        /// </summary>
        public bool IsAutoStart
        {
            get
            {
                return GlobalDataSingleton.Instance.IsAutoStart;
            }
            set
            {
                GlobalDataSingleton.Instance.IsAutoStart = value;
            }
        }
        /// <summary>
        /// 关闭是否最小化
        /// </summary>
        public bool IsActualExit
        {
            get
            {
                return GlobalDataSingleton.Instance.IsActualExit;
            }
            set
            {
                GlobalDataSingleton.Instance.IsActualExit = value;
            }
        }
        /// <summary>
        /// 退出时是否确认
        /// </summary>
        public bool IsExitConfirm
        {
            get
            {
                return GlobalDataSingleton.Instance.IsExitConfirm;
            }
            set
            {
                GlobalDataSingleton.Instance.IsExitConfirm = value;
            }
        }
        public CommonSetting()
        {

        }

        public override void Init()
        {
            Type type = typeof(CommonSetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    CommonSetting common = XmlUtil.Deserialize(type, val) as CommonSetting;

                    this.SetData(this, common);
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }
        }

        public override void Save()
        {
            Type type = typeof(CommonSetting);

            try
            {
                string val = XmlUtil.Serializer(type, this);

                this.Dictionary[type.Name] = val;
            }
            catch { }
        }
    }
}
