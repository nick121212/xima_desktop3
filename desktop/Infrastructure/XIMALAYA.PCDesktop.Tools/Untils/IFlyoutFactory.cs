using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Untils
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFlyoutFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        string GetFlyout(string header);
        /// <summary>
        /// 
        /// </summary>
        void ShowFlyout();
    }
}
