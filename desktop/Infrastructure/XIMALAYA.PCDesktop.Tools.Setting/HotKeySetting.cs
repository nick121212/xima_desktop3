using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DigitalIdeaSolutions.Collections.Generic;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools.MyType;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    public class HotKeySetting : BaseSetting
    {

        public SerializableDictionary<Key, HotKeysManager.CommandKeys> Keys
        {
            get
            {
                return HotKeysManagerSingleton.Instance.Keys;
            }
            set
            {
                HotKeysManagerSingleton.Instance.Keys = value;
            }
        }

        public HotKeySetting()
        {
            this.Keys.Add(System.Windows.Input.Key.F5, HotKeysManager.CommandKeys.Play);
            this.Keys.Add(System.Windows.Input.Key.Right, HotKeysManager.CommandKeys.Next);
            this.Keys.Add(System.Windows.Input.Key.Left, HotKeysManager.CommandKeys.Prev);
            this.Keys.Add(System.Windows.Input.Key.Up, HotKeysManager.CommandKeys.VolumeUp);
            this.Keys.Add(System.Windows.Input.Key.Down, HotKeysManager.CommandKeys.VolumeDown);
            this.Keys.Add(System.Windows.Input.Key.C, HotKeysManager.CommandKeys.Close);
            this.Keys.Add(System.Windows.Input.Key.M, HotKeysManager.CommandKeys.Minisize);
            this.Keys.Add(System.Windows.Input.Key.N, HotKeysManager.CommandKeys.Maxisize);
        }
        public override void Init()
        {
            foreach (var val in this.Keys)
            {
                HotKeysManagerSingleton.Instance.AddKey(val.Key, val.Value);
            }
        }
    }
}
