using System;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace NTP
{
    /// <summary>
    /// DebugWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DebugWindow : MetroWindow
    {
        NST.TelecomImplService service;

        public DebugWindow()
        {
            InitializeComponent();

            service = new NST.TelecomImplService();
        }

        private void DoTry(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                this.ShowMessageAsync("ERROR", ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            service.requestTokenCompleted += (s, args) =>
            {
                DoTry(() => BoxToken.Invoke(() => BoxToken.Text = args.Result));
            };
            service.requestTokenAsync();
        }

        private void BtnFromRequest_Click(object sender, RoutedEventArgs e)
        {
            BoxFromResult.Text = "";

            if (BoxToken.Text.Length == 0)
            {
                this.ShowMessageAsync("ERROR", "token is null");
                return;
            }
            if (BoxFromID.Text.Length == 0 || BoxFromKey.Text.Length == 0)
            {
                this.ShowMessageAsync("ERROR", "something is null");
                return;
            }

            service.fromDeviceCompleted += (s, args) =>
            {
                DoTry(() => BoxFromResult.Invoke(() => BoxFromResult.Text = args.Result));
                DoTry(() => SwitchFromState.Invoke(
                    () => SwitchFromState.IsChecked = args.Result.Contains("info.state:true")
                ));
            };
            service.fromDeviceAsync(BoxToken.Text, BoxFromID.Text, BoxFromKey.Text);
        }

        private void BtnToRequest_Click(object sender, RoutedEventArgs e)
        {
            BoxToResult.Text = "";
            
            if (BoxToken.Text.Length == 0)
            {
                this.ShowMessageAsync("ERROR", "token is null");
                return;
            }
            if (BoxToID.Text.Length == 0 || BoxToKey.Text.Length == 0)
            {
                this.ShowMessageAsync("ERROR", "something is null");
                return;
            }

            service.toDeviceCompleted += (s, args) =>
            {
                DoTry(() => BoxToResult.Invoke(() => BoxToResult.Text = args.Result));
            };
            service.toDeviceAsync(BoxToken.Text, BoxToID.Text, BoxToKey.Text, SwitchToState.IsChecked.Value);
        }

        private void BtnPing_Click(object sender, RoutedEventArgs e)
        {
            BoxPing.Text = "";

            if (BoxToken.Text.Length == 0)
            {
                this.ShowMessageAsync("ERROR", "token is null");
                return;
            }

            service.pingCompleted += (s, args) =>
            {
                DoTry(() => BoxPing.Invoke(() => BoxPing.Text = args.Result));
            };
            service.pingAsync(BoxToken.Text);
        }

    }
}
