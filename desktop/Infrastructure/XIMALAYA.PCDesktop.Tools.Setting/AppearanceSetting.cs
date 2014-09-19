using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools.Themes;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    public class AppearanceSetting : BaseSetting
    {
        public string ThemeName
        {
            get
            {
                return ThemeInfoManagerSingleton.Instance.ThemeName;
            }
            set
            {
                ThemeInfoManagerSingleton.Instance.ThemeName = value;
            }
        }
        public string AccentColorName
        {
            get
            {
                return ThemeInfoManagerSingleton.Instance.AccentColorName;
            }
            set
            {
                ThemeInfoManagerSingleton.Instance.AccentColorName = value;
            }
        }

        public AppearanceSetting()
        {
            //this.ThemeName = "BaseDark";
            //this.AccentColorName = "Blue";
        }

        public override void Init()
        {
            Type type = typeof(AppearanceSetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    AppearanceSetting Appearance = XmlUtil.Deserialize(type, val) as AppearanceSetting;
                    this.SetData(this, Appearance);
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }
        }

        public override void Save()
        {
            Type type = typeof(AppearanceSetting);

            try
            {
                string val = XmlUtil.Serializer(type, this);

                this.Dictionary[type.Name] = val;
            }
            catch { }
        }
    }
}
