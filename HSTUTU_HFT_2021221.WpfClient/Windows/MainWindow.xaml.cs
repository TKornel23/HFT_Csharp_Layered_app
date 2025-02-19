﻿using HSTUTU_HFT_2021221.WpfClient.Windows;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HSTUTU_HFT_2021221.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PostWindow pw = new PostWindow();
            pw.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BlogWindow bw = new BlogWindow();
            bw.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TagWindow tw = new TagWindow();
            tw.ShowDialog();
        }
    }
}
