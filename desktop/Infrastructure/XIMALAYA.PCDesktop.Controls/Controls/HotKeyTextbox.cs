using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 快捷键文本框
    /// </summary>
    [TemplatePart(Name = PART_HotKeyText, Type = typeof(TextBox))]
    public class HotKeyTextbox : ContentControl
    {
        const string PART_HotKeyText = "PART_HotKeyText";

        public TextBox KeyTextBox { get; set; }

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
        public System.Windows.Input.Key HotKey
        {
            get { return (System.Windows.Input.Key)GetValue(HotKeyProperty); }
            set { SetValue(HotKeyProperty, value); }
        }
        /// <summary>
        /// 快捷键
        /// </summary>
        public static readonly DependencyProperty HotKeyProperty =
            DependencyProperty.Register("HotKey", typeof(Key), typeof(HotKeyTextbox), new PropertyMetadata(Key.None));
        /// <summary>
        /// 删除快捷键
        /// </summary>
        public DelegateCommand ClearHotKeyCommand
        {
            get { return (DelegateCommand)GetValue(ClearHotKeyCommandProperty); }
            set { SetValue(ClearHotKeyCommandProperty, value); }
        }
        /// <summary>
        /// 删除快捷键
        /// </summary>
        public static readonly DependencyProperty ClearHotKeyCommandProperty =
            DependencyProperty.Register("ClearHotKeyCommand", typeof(DelegateCommand), typeof(HotKeyTextbox), new PropertyMetadata(null));
        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.KeyTextBox = GetTemplateChild(PART_HotKeyText) as TextBox;
            if (this.KeyTextBox == null) return;

            this.KeyTextBox.KeyUp += KeyTextBox_KeyUp;
        }
        /// <summary>
        /// 构造
        /// </summary>
        public HotKeyTextbox()
            : base()
        {
            this.ClearHotKeyCommand = new DelegateCommand(() =>
            {
                KeyTextBox_KeyUp(null, null);
            });
        }
        void KeyTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Key Key = Key.None;

            if (e != null)
            {
                Key = e.Key;
                int keyCode = (int)e.Key;

                //left,up,right,down
                if (keyCode >= 23 && keyCode <= 26)
                {

                }
                //0-9
                else if (keyCode >= 34 && keyCode <= 43)
                {

                }
                //A-Z
                else if (keyCode >= 44 && keyCode <= 69)
                {

                }
                //F1-F24
                else if (keyCode >= 90 && keyCode <= 113)
                {

                }
                //NUM0-NUM9
                else if (keyCode >= 74 && keyCode <= 83)
                {

                }
                else
                {
                    return;
                }
            }
            if (this.HotKey != Key)
            {
                if (HotKeysManagerSingleton.Instance.AddKey(Key, this.CommandKey))
                {
                    HotKeysManagerSingleton.Instance.DeleteKey(this.HotKey);
                    this.HotKey = Key;
                }
            }
        }
    }
}
