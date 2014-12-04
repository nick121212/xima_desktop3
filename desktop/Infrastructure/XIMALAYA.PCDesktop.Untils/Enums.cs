using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Untils
{
    /// <summary>
    /// 分享网站
    /// </summary>
    public enum Sites
    {
        /// <summary>
        /// 无分享网站，仅复制网址
        /// </summary>
        None,
        /// <summary>
        /// 豆瓣
        /// </summary>
        Douban,
        /// <summary>
        /// 新浪微博
        /// </summary>
        Weibo,
        /// <summary>
        /// MSN
        /// </summary>
        Msn,
        /// <summary>
        /// 开心网
        /// </summary>
        Kaixin,
        /// <summary>
        /// 人人网
        /// </summary>
        Renren,
        /// <summary>
        /// 腾讯微博
        /// </summary>
        TencentWeibo,
        /// <summary>
        /// 饭否
        /// </summary>
        Fanfou,
        /// <summary>
        /// Facebook
        /// </summary>
        Facebook,
        /// <summary>
        /// Twitter
        /// </summary>
        Twitter,
        /// <summary>
        /// QQ空间
        /// </summary>
        Qzone
    }
    /// <summary>
    /// 分享类型
    /// </summary>
    public enum ShareType
    {
        Track,
        Album,
        User
    }
}
