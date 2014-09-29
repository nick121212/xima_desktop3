using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace XIMALAYA.PCDesktop.Untils
{
    /// <summary>
    /// 工具集合
    /// </summary>
    public class Functions
    {
        /// <summary>
        /// 开机自动启动
        /// </summary>
        /// <param name="fileName">启动的文件路径</param>
        /// <param name="isAutoRun">是否自动启动</param>
        public static void SetAutoRun(string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;

            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    throw new Exception("该文件不存在!");

                }
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                {
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

                }
                if (isAutoRun)
                    reg.SetValue(name, fileName);
                else
                    reg.SetValue(name, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (reg != null)
                {
                    reg.Close();
                    reg = null;
                }
            }
        }
    }
}
