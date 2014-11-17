using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Core.Models.User
{
    /// <summary>
    /// 登录用户详情
    /// </summary>
    public class UserData6 : UserData
    {
        public UserData6()
            : base()
        {
            this.doAddMap(() => this.BackGroundLogo, "backgroundPic");
            this.doAddMap(() => this.BirthDay, "birthDay");
            this.doAddMap(() => this.BirthMonth, "birthMonth");
            this.doAddMap(() => this.BirthYear, "birthYear");
            this.doAddMap(() => this.City, "city");
            this.doAddMap(() => this.Country, "country");
            this.doAddMap(() => this.CreateDate, "createTime");
            this.doAddMap(() => this.Email, "email");
            this.doAddMap(() => this.Gender, "gender");
            this.doAddMap(() => this.IsVerified, "isVerified");
            this.doAddMap(() => this.MobileLargeLogo, "largePic");
            this.doAddMap(() => this.LastUpdateDate, "lastModifyTime");
            this.doAddMap(() => this.MobileSmallLogo, "smallPic");
            this.doAddMap(() => this.MobileMiddleLogo, "middlePic");
            this.doAddMap(() => this.NickName, "nickname");
            this.doAddMap(() => this.PersonDescribe, "personDescribe");
            this.doAddMap(() => this.PersonalSignature, "personalSignature");
            this.doAddMap(() => this.Province, "province");
            this.doAddMap(() => this.PTitle, "ptitle");
            this.doAddMap(() => this.Uid, "uid");
        }
    }
}
