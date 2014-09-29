using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XIMALAYA.PCDesktop.Common;

namespace XIMALAYA.PCDesktop.Untils
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
        /// 新建面板
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="veiwModel"></param>
        /// <param name="titlePath"></param>
        /// <returns></returns>
        string GetFlyout(string header, double? Width, double? Height, BaseViewModel veiwModel, Expression<Func<string>> propertyExpression);

        /// <summary>
        /// 显示面板
        /// </summary>
        void ShowFlyout();
    }
}
