using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools.Themes;

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

        }
    }
}
