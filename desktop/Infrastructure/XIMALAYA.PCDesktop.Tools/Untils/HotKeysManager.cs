using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using DigitalIdeaSolutions.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;

namespace XIMALAYA.PCDesktop.Tools.Untils
{
    public class HotKeysManager : NotificationObject, IDisposable
    {
        public enum CommandKeys
        {
            None,
            Play,
            Next,
            Prev,
            VolumeUp,
            VolumeDown,
            Minisize,
            Maxisize,
            Close
        }

        public ObservableDictionary<System.Windows.Forms.Keys, CommandKeys> Keys { get; set; }
        public ObservableDictionary<CommandKeys, ICommand> Commands { get; set; }
        public HotKey.KeyModifiers KeyModifiers { get; set; }
        private IntPtr Handle { get; set; }

        private HotKeysManager()
        {
            HwndSource hWndSource;
            WindowInteropHelper wih = new WindowInteropHelper(Application.Current.MainWindow);

            this.Handle = wih.Handle;
            hWndSource = HwndSource.FromHwnd(this.Handle);
            //添加处理程序
            hWndSource.AddHook(MainWindowProc);
            this.KeyModifiers = HotKey.KeyModifiers.Alt;
            this.Keys = new ObservableDictionary<System.Windows.Forms.Keys, CommandKeys>();
            this.Commands = new ObservableDictionary<CommandKeys, ICommand>();

            this.Commands.Add(CommandKeys.Play, CommandSingleton.Instance.BassEngine.PlayCommand);
            this.Commands.Add(CommandKeys.Next, CommandSingleton.Instance.NextCommand);
            this.Commands.Add(CommandKeys.Prev, CommandSingleton.Instance.PrevCommand);
            this.Commands.Add(CommandKeys.VolumeUp, CommandSingleton.Instance.VolumeUpCommand);
            this.Commands.Add(CommandKeys.VolumeDown, CommandSingleton.Instance.VolumeDownCommand);

            this.Commands.Add(CommandKeys.Minisize, CommandSingleton.Instance.MinisizeCommand);
            this.Commands.Add(CommandKeys.Maxisize, CommandSingleton.Instance.MaxisizeCommand);
            this.Commands.Add(CommandKeys.Close, CommandSingleton.Instance.CloseCommand);
        }
        IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case HotKey.WM_HOTKEY:
                    {
                        int sid = wParam.ToInt32();

                        foreach (System.Windows.Forms.Keys key in this.Keys.Keys)
                        {
                            string keyStr = string.Format("{0}-{1}", this.KeyModifiers.ToString(), key.ToString());
                            int alts = HotKey.GlobalAddAtom(keyStr);

                            if (sid == alts)
                            {
                                if (this.Commands.ContainsKey(this.Keys[key]))
                                {
                                    if (this.Commands[this.Keys[key]].CanExecute(null))
                                    {
                                        this.Commands[this.Keys[key]].Execute(null);
                                    }
                                }
                                break;
                            }
                        }

                        handled = true;
                        break;
                    }
            }

            return IntPtr.Zero;
        }

        public void DeleteKey(System.Windows.Forms.Keys key)
        {
            string keyStr = string.Format("{0}-{1}", this.KeyModifiers.ToString(), key.ToString());
            int alts = HotKey.GlobalAddAtom(keyStr);

            HotKey.GlobalDeleteAtom((short)alts);
            HotKey.UnregisterHotKey(this.Handle, alts);
        }

        public void DeleteKey(int alts)
        {
            HotKey.GlobalDeleteAtom((short)alts);
            HotKey.UnregisterHotKey(this.Handle, alts);
        }

        public void AddKey(System.Windows.Forms.Keys key, CommandKeys command)
        {
            string keyStr = string.Format("{0}-{1}", this.KeyModifiers.ToString(), key.ToString());
            int alts = HotKey.GlobalAddAtom(keyStr);

            if (this.Keys.ContainsKey(key))
            {
                MessageBox.Show("重复的键值！");
                return;
            }

            this.Keys.Add(key, command);
            HotKey.RegisterHotKey(this.Handle, alts, this.KeyModifiers, (int)key);
        }

        #region IDisposable 成员

        public void Dispose()
        {
            foreach (int key in this.Keys.Keys)
            {
                this.DeleteKey(key);
            }
            this.Keys.Clear();
            this.Keys = null;
        }

        #endregion
    }
    /// <summary>
    /// 全局热键管理
    /// </summary>
    public class HotKeysManagerSingleton : Singleton<HotKeysManager> { }
}
