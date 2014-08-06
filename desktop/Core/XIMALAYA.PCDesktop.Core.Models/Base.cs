using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models
{
    public class Base
    {
        /// <summary>
        /// 是否是分页项
        /// </summary>
        public bool IsShowPaging { get; set; }
        /// <summary>
        /// 分页项的文字
        /// </summary>
        public string PagingText { get; set; }
        /// <summary>
        /// 第一个元素
        /// </summary>
        public bool IsFirst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Maps { get; set; }

        public Base()
        {
            this.Maps = new Dictionary<string, string>();
        }
        protected void doAddMap(string key, string value)
        {
            if (this.Maps == null) return;

            if (this.Maps.ContainsKey(key))
            {
                this.Maps[key] = value;
            }
            else
            {
                this.Maps.Add(key, value);
            }
        }
        protected void doAddMap<T>(Expression<Func<T>> propertyExpression, string value)
        {
            if (this.Maps == null) return;
            var body = propertyExpression.Body as MemberExpression;

            this.doAddMap(body.Member.Name, value);
        }
    }
}
