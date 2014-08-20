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
    public interface IBaseResult
    {
        int Ret { get; set; }
        string Message { get; set; }
    }
}
