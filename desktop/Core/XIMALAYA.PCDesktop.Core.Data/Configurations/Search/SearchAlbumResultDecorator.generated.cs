﻿// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 SearchAlbumResultConfigurationAppend 分部方法。
// </auto-generated>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Search;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    ///     SearchAlbumResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class SearchAlbumResultDecorator<T> : Decorator<T>
    {
        partial void doAddOtherConfig();
        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="result"></typeparam>
        public SearchAlbumResultDecorator(Result<T> result)
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
            this.Config.MapType<SearchAlbumResult>(map => map
                                    .Field<System.Int32>(field => field.Ret, type => type.To("ret"))
                    .Field<System.String>(field => field.Message, type => type.To("msg"))
                    .Field<System.Int64>(field => field.StartAt, type => type.To("start"))
                    .Field<System.Int64>(field => field.TotalCount, type => type.To("numFound"))
                    .Field<XIMALAYA.PCDesktop.Core.Models.Album.AlbumData[]>(field => field.Albums, type => type.To("docs"))
            );
            this.doAddOtherConfig();
        }
    }
}
