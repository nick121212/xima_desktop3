using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    public class LikedSoundUserResult : BaseResult
    {
        public int MaxPageId { get; set; }
        public long TotalCount { get; set; }
        public int PageId { get; set; }
        public int PageSize { get; set; }
        public UserData[] Users { get; set; }
        public LikedSoundUserResult()
        {
            this.doAddMap(() => this.MaxPageId, "maxPageId");
            this.doAddMap(() => this.PageId, "pageId");
            this.doAddMap(() => this.PageSize, "pageSize");
            this.doAddMap(() => this.TotalCount, "totalCount");
            this.doAddMap(() => this.Users, "list");
        }
    }
}
