﻿// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 SoundDetailResultConfigurationAppend 分部方法。
// </auto-generated>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentJson.Configuration;
using FluentJson;
using XIMALAYA.PCDesktop.Core.Data.Decorator;
using XIMALAYA.PCDesktop.Core.Models.Sound;

namespace XIMALAYA.PCDesktop.Core.Data
{
    /// <summary>
    ///     SoundDetailResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class SoundDetailResultDecorator<T> : Decorator<T>
    {
        partial void doAddOtherConfig();
        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="result"></typeparam>
        public SoundDetailResultDecorator(Result<T> result)
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
            this.Config.MapType<SoundData>(map => map
                                    .Field<System.Int64>(field => field.TrackId, type => type.To("trackId"))
                    .Field<System.String>(field => field.Title, type => type.To("title"))
                    .Field<System.Int64>(field => field.CategoryID, type => type.To("categoryId"))
                    .Field<System.String>(field => field.CategoryName, type => type.To("categoryName"))
                    .Field<System.String>(field => field.PlayUrl64, type => type.To("playUrl64"))
                    .Field<System.String>(field => field.PlayUrl32, type => type.To("playUrl32"))
                    .Field<System.Int32>(field => field.ProcessState, type => type.To("processState"))
                    .Field<System.Double>(field => field.Duration, type => type.To("duration"))
                    .Field<System.String>(field => field.CoverSmall, type => type.To("coverSmall"))
                    .Field<System.String>(field => field.CoverLarge, type => type.To("coverLarge"))
                    .Field<System.Int64>(field => field.CreateAt, type => type.To("createdAt"))
                    .Field<System.Int64>(field => field.UID, type => type.To("uid"))
                    .Field<System.Int32>(field => field.UserSource, type => type.To("userSource"))
                    .Field<System.Int64>(field => field.AlbumID, type => type.To("albumId"))
                    .Field<System.String>(field => field.AlbumTitle, type => type.To("albumTitle"))
                    .Field<System.String>(field => field.AlbumImage, type => type.To("albumImage"))
                    .Field<System.Int32>(field => field.OpType, type => type.To("opType"))
                    .Field<System.Boolean>(field => field.IsPlulic, type => type.To("isPublic"))
                    .Field<System.String[]>(field => field.Images, type => type.To("images"))
                    .Field<System.String>(field => field.Intro, type => type.To("intro"))
                    .Field<System.String>(field => field.Tags, type => type.To("tags"))
                    .Field<System.String>(field => field.Lyric, type => type.To("lyric"))
                    .Field<System.String>(field => field.RichIntro, type => type.To("richIntro"))
                    .Field<System.Boolean>(field => field.IsRelay, type => type.To("isRelay"))
                    .Field<System.Boolean>(field => field.IsLike, type => type.To("isLike"))
                    .Field<System.Int64>(field => field.LikeCount, type => type.To("likes"))
                    .Field<System.Int64>(field => field.PlayCount, type => type.To("playtimes"))
                    .Field<System.Int64>(field => field.CommentCount, type => type.To("comments"))
                    .Field<System.Int64>(field => field.ShareCount, type => type.To("shares"))
                    .Field<System.Object[]>(field => field.TrackBlocks, type => type.To("trackBlocks"))
                    .Field<XIMALAYA.PCDesktop.Core.Models.User.UserData>(field => field.User, type => type.To("userInfo"))
            );
            this.doAddOtherConfig();
        }
    }
}
