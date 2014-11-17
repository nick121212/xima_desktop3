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
        /// 如果是TilePanel子元素的话，设置宽度的比例
        /// </summary>
        public int WidthPix { get; set; }
        /// <summary>
        /// 如果是TilePanel子元素的话，设置高度的比例
        /// </summary>
        public int HeightPix { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Maps { get; set; }
        /// <summary>
        /// 成功标志
        /// </summary>
        public int Ret { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        public Base()
        {
            this.Maps = new Dictionary<string, string>();
            this.WidthPix = 1;
            this.HeightPix = 1;
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
