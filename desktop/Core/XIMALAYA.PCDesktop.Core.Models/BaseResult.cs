using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models
{
    /// <summary>
    /// 基类
    /// </summary>
    public abstract class BaseResult : Base
    {
        private bool _res = false;
        /// <summary>
        /// 成功标志
        /// </summary>
        public int Ret { get; set; }
        /// <summary>
        /// 成功标志
        /// </summary>
        public bool Res
        {
            get
            {
                return _res;
            }
            set
            {
                _res = value;
                this.Ret = value ? 0 : 1;
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        public BaseResult()
            : base()
        {
            this.doAddMap(() => this.Ret, "ret");
            this.doAddMap(() => this.Res, "res");
            this.doAddMap(() => this.Message, "msg");
        }
    }
}
