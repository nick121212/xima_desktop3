﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Events;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools;

namespace XIMALAYA.PCDesktop.Modules.Settings.Views
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class SettingsView
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        [Import]
        public SettingsViewModel ViewModel
        {
            get { return this.DataContext as SettingsViewModel; }
            set { this.DataContext = value; }
        }
    }
}
