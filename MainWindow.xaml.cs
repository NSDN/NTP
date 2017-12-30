using System;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;

using MahApps.Metro.Controls;

namespace NTP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            new EditorWindow().Show();
        }

        private void BtnDebug_Click(object sender, RoutedEventArgs e)
        {
            new DebugWindow().ShowDialog();
        }

        private void BtnLogo_Click(object sender, RoutedEventArgs e)
        {
            InfoPanel.IsOpen = !InfoPanel.IsOpen;
        }

    }
}
