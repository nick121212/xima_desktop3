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

namespace XIMALAYA.PCDesktop.Modules.DiscoverModule.Views
{
    /// <summary>
    /// CategoryDetailView.xaml 的交互逻辑
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CategoryDetailView
    {
        public CategoryDetailView()
        {
            InitializeComponent();
            //this.Unloaded += CategoryDetailView_Unloaded;
        }

        void CategoryDetailView_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Dispose();
            this.ViewModel = null;
            GC.Collect();
        }
        [Import]
        public CategoryDetailViewModel ViewModel
        {
            get { return this.DataContext as CategoryDetailViewModel; }
            set { this.DataContext = value; }
        }

    }
}
