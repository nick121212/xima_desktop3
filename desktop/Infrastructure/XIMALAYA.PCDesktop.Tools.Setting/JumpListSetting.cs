using System.Collections.Generic;
using System.Windows;
using System.Windows.Shell;
using System.Xml.Serialization;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    [XmlInclude(typeof(JumpTask))]
    public class JumpListSetting : BaseSetting
    {
        public List<JumpItem> JumpItemList { get; set; }
        [XmlIgnore]
        public JumpList Jumplist { get; set; }

        public JumpListSetting()
        {
            this.JumpItemList = new List<JumpItem>();
        }

        public override void Init()
        {
            this.Jumplist = JumpList.GetJumpList(Application.Current);

            if (this.Jumplist == null) this.Jumplist = new JumpList();

            this.Jumplist.ShowFrequentCategory = false;
            this.Jumplist.ShowRecentCategory = false;
            this.Jumplist.JumpItems.Clear();
            this.Jumplist.JumpItems.AddRange(JumpItemList);

            JumpList.SetJumpList(Application.Current, this.Jumplist);
        }
    }
}
