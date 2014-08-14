using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.Album
{
    /// <summary>
    /// 用于搜索专辑
    /// </summary>
    public class AlbumData4 : AlbumData
    {
        public AlbumData4()
            : base()
        {
            this.doAddMap(() => this.CategoryID, "category_id");
            this.doAddMap(() => this.CoverPath, "cover_path");
            this.doAddMap(() => this.CreateDate, "created_at");
            this.doAddMap(() => this.AlbumID, "id");
            this.doAddMap(() => this.Intro, "intro");
            this.doAddMap(() => this.IsVerified, "is_v");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.Title, "title");
            this.doAddMap(() => this.TrackCount, "tracks");
            this.doAddMap(() => this.Uid, "uid");
            this.doAddMap(() => this.UpdateDate, "updated_at");
            //this.doAddMap(() => this.UserSource, "user_source");
        }
    }
}
