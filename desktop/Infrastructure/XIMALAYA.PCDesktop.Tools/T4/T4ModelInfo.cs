using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.T4
{
    /// <summary>
    /// T4实体模型信息类
    /// </summary>
    public class T4ModelInfo
    {
        /// <summary>
        /// 获取 是否使用模块文件夹
        /// </summary>
        public bool UseModuleDir { get; private set; }

        /// <summary>
        /// 获取 模型所在模块名称
        /// </summary>
        public string ModuleName { get; private set; }

        /// <summary>
        /// 获取 模型名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取 模型描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 主键类型
        /// </summary>
        public Type KeyType { get; set; }

        /// <summary>
        /// 主键类型名称
        /// </summary>
        public string KeyTypeName { get; set; }
        /// <summary>
        /// 映射的类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 字段关联地图
        /// </summary>
        public Dictionary<string, string> Maps { get; set; }
        /// <summary>
        /// 属性集合
        /// </summary>
        public IEnumerable<PropertyInfo> Properties { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="useModuleDir"></param>
        public T4ModelInfo(Type modelType, bool useModuleDir = false)
        {
            var @namespace = modelType.Namespace;
            if (@namespace == null)
            {
                return;
            }
            UseModuleDir = useModuleDir;
            if (UseModuleDir)
            {
                var index = @namespace.LastIndexOf('.') + 1;
                ModuleName = @namespace.Substring(index, @namespace.Length - index);

                if (ModuleName.ToLower() == "models")
                {
                    UseModuleDir = false;
                }
            }

            Name = modelType.Name;
            
            PropertyInfo keyProp = modelType.GetProperty("Maps");

            if (keyProp != null)
            {
                KeyType = keyProp.PropertyType;
                KeyTypeName = KeyType.Name;
                try
                {
                    object t = Activator.CreateInstance(modelType);
                    this.Maps = keyProp.GetValue(t) as Dictionary<string, string>;
                    if (this.Maps.ContainsKey("FXClassName"))
                    {
                        this.ClassName = this.Maps["FXClassName"];
                        this.Maps.Remove("FXClassName");
                    }
                    else
                    {
                        this.ClassName = this.Name;
                    }
                }
                catch(Exception e) {
                    this.Maps = new Dictionary<string, string>();
                    System.Windows.MessageBox.Show(e.Message);
                }
            }

            var descAttributes = modelType.GetCustomAttributes(typeof(DescriptionAttribute), true);
            Description = descAttributes.Length == 1 ? ((DescriptionAttribute)descAttributes[0]).Description : Name;
            Properties = modelType.GetProperties();
        }
    }
}
