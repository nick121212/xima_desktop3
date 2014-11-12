using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutex { get; set; }

        protected void WriteToMappedFile(string args)
        {
            try
            {
                //using (MemoryMappedFile mappedFile = MemoryMappedFile.OpenExisting(_mappedFileName))
                using (Stream stream = GlobalDataSingleton.Instance.MemoryMappedFile.CreateViewStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, args);
                }
            }
            catch { }
        }
        [STAThread]
        protected override void OnStartup(StartupEventArgs e)
        {
            //只允许运行一个实例
            bool createdNew = false;
            FileInfo file = new FileInfo(Assembly.GetExecutingAssembly().Location);

            Environment.CurrentDirectory = file.DirectoryName;

            GlobalDataSingleton.Instance.ExeFileLocation = file.FullName;
            mutex = new Mutex(true, "{ximalaya-thirdpart}", out createdNew);

            if (e.Args != null && e.Args.Length > 0)
            {
                WriteToMappedFile(e.Args[0]);
            }
            else
            {
                WriteToMappedFile(string.Empty);
            }

            if (!createdNew)
            {
                Debug.WriteLine("**********************************************************************");
                Debug.WriteLine("检测到已有一个喜马拉雅第三方客户端在运行，程序将关闭");
                Debug.WriteLine("**********************************************************************");
                Shutdown(0);
                return;
            }

            //设置调试输出
            Debug.AutoFlush = true;
            Debug.Listeners.Add(new TextWriterTraceListener("ximalaya.log"));

            Debug.WriteLine(string.Empty);
            Debug.WriteLine("**********************************************************************");
            Debug.WriteLine("喜马拉雅第三方客户端启动时间：" + DateTime.Now);
            Debug.WriteLine("**********************************************************************");
            Debug.WriteLine(string.Empty);

            Exit += new ExitEventHandler((sender, e1) =>
            {
                mutex.Close();
                mutex = null;
                Debug.WriteLine(DateTime.Now.ToShortDateString() + " 程序结束，返回代码为" + e1.ApplicationExitCode);
            });

            System.Windows.Media.RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;

            /* 这句话可以使Global User Interface这个默认的组合字体按当前系统的区域信息选择合适的字形。
             * 只对FrameworkElement有效。对于FlowDocument，由于是从FrameworkContentElement继承，
             * 而且FrameworkContentElement.LanguageProperty.OverrideMetadata()无法再次执行，
             * 目前我知道的最好的办法是在使用了FlowDocument的XAML的根元素上加上xml:lang="zh-CN"，
             * 这样就能强制Global User Interface在FlowDocument上使用大陆的字形。
             * */
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name)));

            base.OnStartup(e);
            new MyBootstrapper().Run();
        }
    }
}
