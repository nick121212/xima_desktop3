using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using DigitalIdeaSolutions.Collections.Generic;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Tools.MyType;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Tools.HotKey
{
    public class HotKeysManager : NotificationObject, IDisposable
    {
        [Serializable]
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

        public SerializableDictionary<System.Windows.Input.Key, CommandKeys> Keys { get; set; }
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
            this.Keys = new SerializableDictionary<System.Windows.Input.Key, CommandKeys>();
            this.Commands = new ObservableDictionary<CommandKeys, ICommand>();

            this.Commands.Add(CommandKeys.Play, GlobalDataSingleton.Instance.BassEngine.PlayCommand);
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

                        foreach (System.Windows.Input.Key key in this.Keys.Keys)
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

        public void DeleteKey(System.Windows.Input.Key key)
        {
            string keyStr = string.Format("{0}-{1}", this.KeyModifiers.ToString(), key.ToString());
            int alts = HotKey.GlobalAddAtom(keyStr);

            HotKey.GlobalDeleteAtom((short)alts);
            HotKey.UnregisterHotKey(this.Handle, alts);

            if (this.Keys.ContainsKey(key))
            {
                this.Keys.Remove(key);
            }
        }

        public bool AddKey(System.Windows.Input.Key key, CommandKeys command)
        {
            string keyStr = string.Format("{0}-{1}", this.KeyModifiers.ToString(), key.ToString());
            int alts = HotKey.GlobalAddAtom(keyStr);

            if (key != Key.None)
            {
                try
                {
                    System.Windows.Forms.Keys keyF = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), key.ToString());
                    if (!HotKey.RegisterHotKey(this.Handle, alts, this.KeyModifiers, (int)keyF))
                    {
                        HotKey.GlobalDeleteAtom((short)alts);
                        string msg = string.Empty;
                        int errorCode = Marshal.GetLastWin32Error();
                        switch (errorCode)
                        {
                            case 1409:
                                msg = "热键[" + keyF .ToString()+ "]已经注册！";
                                break;
                            default:
                                msg = errorCode.ToString();
                                break;
                        }
                        //DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "喜马拉雅", msg);
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            if (!this.Keys.ContainsKey(key))
            {
                this.Keys.Add(key, command);
            }
            return true;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            foreach (Key key in this.Keys.Keys)
            {
                this.DeleteKey(key);
            }
            this.Keys.Clear();
            this.Keys = null;
        }

        #endregion
    }

}
