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
    public abstract class BaseResult : Base, IBaseResult
    {
        private bool _res = false;
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

        public BaseResult()
            : base()
        {
            this.doAddMap(() => this.Ret, "ret");
            this.doAddMap(() => this.Message, "msg");
        }
    }
}
