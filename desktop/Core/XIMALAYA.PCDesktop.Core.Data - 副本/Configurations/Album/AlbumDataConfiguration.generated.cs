﻿// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 AlbumDataConfigurationAppend 分部方法。
// </auto-generated>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Models.Album;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public partial class AlbumDataConfigDecorator<T, T1> : JsonConfigDecorator<T, T1>
        where T : AlbumData
        where T1 : class
    {

        /// <summary>
        /// 添加配置
        /// </summary>
        partial void AppendConfig();
        /// <summary>
        /// 配置类
        /// </summary>
        public AlbumDataConfigDecorator(IJsonConfig decorator, JsonConfiguration<T1> config) : base(decorator, config) { }
       
        /// <summary>
        /// 添加配置
        /// </summary>
        public override void AddConfig()
        {
            base.AddConfig();
            this.AppendConfig();
        }
    }
}
