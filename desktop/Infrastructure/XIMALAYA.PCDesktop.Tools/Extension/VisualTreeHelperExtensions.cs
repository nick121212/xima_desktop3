using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace XIMALAYA.PCDesktop.Tools.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class VisualTreeHelperExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependencyObject"></param>
        /// <returns></returns>
        public static T FindAncestor<T>(DependencyObject dependencyObject)
            where T : class
        {
            DependencyObject target = dependencyObject;
            do
            {
                target = VisualTreeHelper.GetParent(target);
            }
            while (target != null && !(target is T));
            return target as T;
        }
    }
}
