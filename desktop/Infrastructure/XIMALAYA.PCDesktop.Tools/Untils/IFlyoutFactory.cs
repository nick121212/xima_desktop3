using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Untils
{
    /// <summary>
    /// 面板接口
    /// </summary>
    public interface IFlyoutFactory
    {
        /// <summary>
        /// 新建面板
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        string GetFlyout(string header);
        /// <summary>
        /// 新建面板
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        string GetFlyout(string header, double? Width, double? Height);
        /// <summary>
        /// 新建面板
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="isModal"></param>
        /// <returns></returns>
        string GetFlyout(string header, double? Width, double? Height, bool isModal);
        /// <summary>
        /// 显示面板
        /// </summary>
        void ShowFlyout();
    }
}
