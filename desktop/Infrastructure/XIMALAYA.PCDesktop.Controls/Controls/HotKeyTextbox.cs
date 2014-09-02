using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 快捷键文本框
    /// </summary>
    public class HotKeyTextbox : ContentControl
    {
        /// <summary>
        /// 命令KEY
        /// </summary>
        public HotKeysManager.CommandKeys CommandKey
        {
            get { return (HotKeysManager.CommandKeys)GetValue(CommandKeyProperty); }
            set { SetValue(CommandKeyProperty, value); }
        }
        /// <summary>
        /// 命令KEY
        /// </summary>
        public static readonly DependencyProperty CommandKeyProperty =
            DependencyProperty.Register("CommandKey", typeof(HotKeysManager.CommandKeys), typeof(HotKeyTextbox), new PropertyMetadata(HotKeysManager.CommandKeys.None));
         /// <summary>
        /// 快捷键
        /// </summary>
        public System.Windows.Forms.Keys HotKey
        {
            get { return (System.Windows.Forms.Keys)GetValue(HotKeyProperty); }
            set { SetValue(HotKeyProperty, value); }
        }
         /// <summary>
        /// 快捷键
        /// </summary>
        public static readonly DependencyProperty HotKeyProperty =
            DependencyProperty.Register("HotKey", typeof(System.Windows.Forms.Keys), typeof(HotKeyTextbox),new PropertyMetadata(System.Windows.Forms.Keys.None, OnKeyChanged));

        private static void OnKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HotKeyTextbox textbox = d as HotKeyTextbox;

            if (e.NewValue != e.OldValue)
            {
                HotKeysManagerSingleton.Instance.DeleteKey((System.Windows.Forms.Keys)e.OldValue);
                HotKeysManagerSingleton.Instance.AddKey((System.Windows.Forms.Keys)e.NewValue, textbox.CommandKey);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

    }
}
