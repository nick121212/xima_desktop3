﻿// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 SoundData6ConfigurationAppend 分部方法。
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

    partial class SoundData6Decorator<T>
    {
        partial void doAddOtherConfig()
        {
            this.Config.MapType<SoundData>(map => map
                .Field<System.Double>(field => field.Duration, type => type.To("duration").DecodeAs<string>(value => Double.Parse(value))));
        }
    }

}
