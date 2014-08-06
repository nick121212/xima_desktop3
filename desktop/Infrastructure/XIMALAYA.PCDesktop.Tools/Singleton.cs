using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace XIMALAYA.PCDesktop.Tools
{
    /// <summary>
    /// 泛型单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : class
    {
        private static readonly Lazy<T> _instance =
            new Lazy<T>(() =>
            {
                var ctors = typeof(T).GetConstructors(BindingFlags.Instance 
                    | BindingFlags.NonPublic 
                    | BindingFlags.Public);

                if (ctors.Count() != 1)
                {
                    throw new InvalidOperationException("只能有一个构造函数！");
                }
                var ctor = ctors.SingleOrDefault(c => c.GetParameters().Count() == 0 && c.IsPrivate);
                if (ctor == null)
                {
                    throw new InvalidOperationException("必须得有私有且不带参数的构造函数");
                }

                return (T)ctor.Invoke(null);
            });

        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
