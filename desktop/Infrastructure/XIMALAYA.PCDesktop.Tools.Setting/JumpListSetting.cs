using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shell;
using System.Xml.Serialization;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Untils;

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
            Type type = typeof(JumpListSetting);

            if (this.Dictionary.ContainsKey(type.Name))
            {
                try
                {
                    string val = this.Dictionary[type.Name];

                    var Jumplist = XmlUtil.Deserialize(type, val) as JumpListSetting;

                    this.SetData(this, Jumplist);
                }
                catch
                {
                    this.Dictionary.Remove(type.Name);
                }
            }

            this.Jumplist = JumpList.GetJumpList(Application.Current);

            if (this.Jumplist == null) this.Jumplist = new JumpList();

            this.Jumplist.ShowFrequentCategory = false;
            this.Jumplist.ShowRecentCategory = false;
            this.Jumplist.JumpItems.Clear();
            this.Jumplist.JumpItems.AddRange(JumpItemList);

            JumpList.SetJumpList(Application.Current, this.Jumplist);
        }

        public override void Save()
        {
            Type type = typeof(JumpListSetting);

            try
            {
                this.JumpItemList = this.Jumplist.JumpItems;
                string val = XmlUtil.Serializer(type, this.Jumplist);

                this.Dictionary[type.Name] = val;
            }
            catch { }
        }
    }
}
