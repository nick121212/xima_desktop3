﻿// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 ShareResultConfigurationAppend 分部方法。
// </auto-generated>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Share;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    ///     ShareResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ShareResultDecorator<T> : Decorator<T>
    {
        partial void doAddOtherConfig();
        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="result"></typeparam>
        public ShareResultDecorator(Result<T> result)
            : base(result)
        {

        }
        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="result"></typeparam>
        public override void doAddConfig()
        {
            base.doAddConfig();
            this.Config.MapType<ShareResult>(map => map
                                    .Field<System.Int32>(field => field.Ret, type => type.To("ret"))
                    .Field<System.String>(field => field.Message, type => type.To("msg"))
                    .Field<System.String>(field => field.Content, type => type.To("content"))
                    .Field<System.Int32>(field => field.Limit, type => type.To("lengthLimit"))
                    .Field<System.String>(field => field.PicUrl, type => type.To("picUrl"))
                    .Field<System.String>(field => field.Url, type => type.To("url"))
                    .Field<System.String>(field => field.WeiXinPic, type => type.To("weixinPic"))
                    .Field<System.String>(field => field.AudioUrl, type => type.To("audioUrl"))
                    .Field<System.String>(field => field.NickName, type => type.To("nickname"))
            );
            this.doAddOtherConfig();
        }
    }
}