using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Share
{
    public class ShareResult : BaseResult
    {
        public string Content { get; set; }
        public int Limit { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
        public string WeiXinPic { get; set; }
        public string AudioUrl { get; set; }
        public string NickName { get; set; }

        public ShareResult()
        {
            this.doAddMap("FXClassName", "ShareResult");
            this.doAddMap(() => this.Content, "content");
            this.doAddMap(() => this.Limit, "lengthLimit");
            this.doAddMap(() => this.PicUrl, "picUrl");
            this.doAddMap(() => this.Url, "url");
            this.doAddMap(() => this.WeiXinPic, "weixinPic");
            this.doAddMap(() => this.AudioUrl,"audioUrl");
            this.doAddMap(() => this.NickName, "nickname");
        }
    }
}
