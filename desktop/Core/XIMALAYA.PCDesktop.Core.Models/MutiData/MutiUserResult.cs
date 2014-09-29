using XIMALAYA.PCDesktop.Core.Models.User;

namespace XIMALAYA.PCDesktop.Core.Models.MutiData
{
    public class MutiUserResult : BaseResult
    {
        public long TotalCount { get; set; }
        public string LongTitle { get; set; }
        public string ShortTitle { get; set; }
        public UserData[] Users { get; set; }

        public MutiUserResult()
        {
            this.doAddMap(() => this.TotalCount, "count");
            this.doAddMap(() => this.LongTitle, "long_title");
            this.doAddMap(() => this.ShortTitle, "short_title");
            this.doAddMap(() => this.Users, "list");
        }
    }
}
