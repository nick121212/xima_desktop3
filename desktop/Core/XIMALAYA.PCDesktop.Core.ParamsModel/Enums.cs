using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// iphone
        /// </summary>
        iphone,
        /// <summary>
        /// ipad
        /// </summary>
        ipad,
        /// <summary>
        /// android
        /// </summary>
        android,
        /// <summary>
        /// chezai_android
        /// </summary>
        chezai_android,
        /// <summary>
        /// wp
        /// </summary>
        wp,
        /// <summary>
        /// pc
        /// </summary>
        pc
    }
    /// <summary>
    /// 标签的类型
    /// </summary>
    public enum TagType
    {
        /// <summary>
        /// 声音标签
        /// </summary>
        sound,
        /// <summary>
        /// 专辑标签
        /// </summary>
        album
    }
    /// <summary>
    /// 查询条件
    /// </summary>
    public enum ConditionSoundType
    {
        /// <summary>
        /// 最新上传的声音
        /// </summary>
        zuixinshangchuan,
        /// <summary>
        /// 最新上传的声音
        /// </summary>
        zuiduoshoucang,
        /// <summary>
        /// jincaituijian
        /// </summary>
        jincaituijian
    }
    /// <summary>
    /// 查询条件
    /// </summary>
    public enum ConditionAlbumType
    {
        /// <summary>
        /// 最新上传的声音
        /// </summary>
        hot,
        /// <summary>
        /// 最新上传的声音
        /// </summary>
        recent,
        /// <summary>
        /// jincaituijian
        /// </summary>
        classic
    }
}
