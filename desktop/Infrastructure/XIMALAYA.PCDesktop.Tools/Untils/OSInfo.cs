using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Untils
{
    /// <summary>
    /// 操作系统PlatformID主版本号副版本号    
    /// </summary>
    public enum OsVer
    {
        /// <summary>
        /// 
        /// </summary>
        NONE = 0,
        /// <summary>
        /// /
        /// </summary>
        Windows95 = 140,
        /// <summary>
        /// 
        /// </summary>
        Windows98 = 1410,
        /// <summary>
        /// 
        /// </summary>
        WindowsMe = 1490,
        /// <summary>
        /// 
        /// </summary>
        WindowsNT35 = 230,
        /// <summary>
        /// 
        /// </summary>
        WindowsNT40 = 240,
        /// <summary>
        /// 
        /// </summary>
        Windows2000 = 250,
        /// <summary>
        /// 
        /// </summary>
        WindowsXP = 251,
        /// <summary>
        /// 
        /// </summary>
        Windows2003 = 252,
        /// <summary>
        /// 
        /// </summary>
        WindowsVista = 260,
        /// <summary>
        /// 
        /// </summary>
        Windows7 = 261,
        /// <summary>
        /// 
        /// </summary>
        Windows8 = 271
    }
    /// <summary>
    /// 
    /// </summary>
    public class OSInfo : Singleton<OSInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        public OperatingSystem OsInfo { get { return Environment.OSVersion; } }
        /// <summary>
        /// 
        /// </summary>
        public PlatformID PlatFormID { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public OsVer OSVersion { get; private set; }

        private OSInfo()
        {
            this.GetOsInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetOsInfo()
        {
            //判断
            string iniPath = System.Environment.GetFolderPath(Environment.SpecialFolder.System);

            //获取操作系统ID
            this.PlatFormID = this.OsInfo.Platform;
            //获取主版本号
            int versionMajor = this.OsInfo.Version.Major;
            //获取副版本号
            int versionMinor = this.OsInfo.Version.Minor;
            string osInfor = this.PlatFormID.GetHashCode().ToString() + versionMajor.ToString() + versionMinor.ToString();

            try
            {
                this.OSVersion = (OsVer)int.Parse(osInfor);
            }
            catch 
            {
            }


            //if (osInfor == OsVer.Windows7.GetHashCode().ToString())
            //{
            //    iniPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //}
            //if (osInfor == OsVer.WindowsXP.GetHashCode().ToString())
            //{
            //    iniPath = System.Environment.GetFolderPath(Environment.SpecialFolder.System);
            //}

            return iniPath;
        }
    }
}
