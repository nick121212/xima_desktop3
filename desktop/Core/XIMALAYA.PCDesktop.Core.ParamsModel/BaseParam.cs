using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Tools.Attributes;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// 参数类基类
    /// </summary>
    public class BaseParam : NotificationObject
    {
        /// <summary>
        /// 分页的页码
        /// </summary>
        [DataMember(IsRequired = false, Name = "page",Order = 20)]
        public int? Page { get; set; }
        /// <summary>
        /// 分页每页的数量
        /// </summary>
        [DataMember(IsRequired = false, Name = "per_page", Order = 30)]
        public int? PerPage { get; set; }
        /// <summary>
        /// 设备名称 
        /// true
        /// </summary>
        [DataMember(IsRequired = false, Name = "device", Order = 10)]
        public DeviceType? Device { get; set; }
        ///// <summary>
        ///// 登陆用户的id
        ///// </summary>
        //[DataMember(IsRequired = false, Name = "uid")]
        //public int? Uid { get; set; }
        ///// <summary>
        ///// 参数列表
        ///// </summary>
        //[ImportMany(AllowRecomposition = true)]
        //protected Lazy<object, IDataMember>[] Params { get; set; }
        /// <summary>
        /// 返回成字符
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sReturn = new StringBuilder();
            List<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttributes(typeof(DataMemberAttribute)).Count() > 0).OrderBy(p => p.GetCustomAttribute<DataMemberAttribute>().Order).ToList();
            int index = 0;
            object val;

            foreach (var ve in properties)
            {
                DataMemberAttribute va = ve.GetCustomAttribute<DataMemberAttribute>();

                if (va != null)
                {
                    val = ve.GetValue(this);
                    if (val == null )//|| val.ToString() == string.Empty)
                    {
                        if (va.IsRequired)
                        {
                            throw new ArgumentNullException(va.Name);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (index > 0)
                    {
                        sReturn.Append("&");
                    }
                    sReturn.AppendFormat("{0}={1}", va.Name, val.ToString());
                    index++;
                }
            }


            //if (this.Params == null)
            //{
            //    throw new ArgumentNullException("Params can not be null!");
            //}
            //foreach (var ve in this.Params)
            //{
            //    if (ve.Metadata.IsRequired && !ve.IsValueCreated )
            //    {
            //        throw new ArgumentNullException(ve.Metadata.Name);
            //    }
            //    if (index > 0)
            //    {
            //        sReturn.Append("&");
            //    }
            //    sReturn.AppendFormat("{0}={1}", ve.Metadata.Name, ve.Value.ToString());
            //    index++;
            //}

            return sReturn.ToString();
        }
    }
}
