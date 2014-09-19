using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Isam.Esent.Collections.Generic;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Tools.Setting
{
    [InheritedExport(typeof(BaseSetting))]
    public abstract class BaseSetting
    {
        [Import("Dictionary")]
        protected PersistentDictionary<string, string> Dictionary { get; set; }
        public abstract void Init();
        public abstract void Save();

        public BaseSetting()
        {
            
        }
        protected void SetData<T>(T oldData, T newData)
        {
            object newValue;

            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.CanWrite)
                {
                    newValue = property.GetValue(newData);

                    if (newValue != null && newValue != this.DefaultForType(newValue.GetType()))
                    {
                        property.SetValue(oldData, newValue);
                    }
                }
            }
        }
        private object DefaultForType(Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }
    }
}
