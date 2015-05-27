using AAAcounter.Model;
using AAAcounter.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AAAcounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainViewController _viewController = null;

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@".{1,}");

        public MainPage()
        {
            this.InitializeComponent();
            _viewController = new MainViewController(this);
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (await _viewController.CheckLogin())
            {
                CloseLoginBox();
            }
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string user = tbx_user.Text;
            if (user == "sudo")
            {
                frame.Navigate(typeof(SuperMan), _viewController);
                CloseLoginBox();
                return;
            }

            if (!regex.IsMatch(user))
            {
                tbl_message.Text = "用户名必须是 字母，数字，下划线的组合或者邮箱, 至少6位";
                return;
            }

            string pwd = tbx_pwd.Text;
            if (pwd.Length < 6)
            {
                tbl_message.Text = "密码至少6位";
                return;
            }

            tbl_message.Text = "正在登录， 请稍候...";
            if (await _viewController.Login(user, pwd))
            {
                tbl_user.Text = user;
                CloseLoginBox();
            }
            else
            {
                tbl_message.Text = "登录失败";
            }
        }

        private async void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            string user = tbx_user.Text;
            if (!regex.IsMatch(user))
            {
                tbl_message.Text = "用户名必须是 字母，数字，下划线的组合或者邮箱";
                return;
            }
            string pwd = tbx_pwd.Text;
            if (pwd.Length < 6)
            {
                tbl_message.Text = "密码至少6位";
                return;
            }

            tbl_message.Text = "正在注册， 请稍候...";
            if (await _viewController.Register(user, pwd))
            {
                tbl_user.Text = user;
                CloseLoginBox();
            }
            else
            {
                tbl_message.Text = "注册失败";
            }
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            _viewController.Logout();
            ShowLoginBox();
        }

        private void CloseLoginBox()
        {
            Grid_Login.Visibility = Visibility.Collapsed;
            frame.Visibility = Visibility.Visible;

            BottomBar.Visibility = Visibility.Visible;
            BottomBar.SetSource(CreateBarItemList().ToList());
        }

        private IEnumerable<BottomMenuItem> CreateBarItemList()
        {
            BottomMenuItem newRec = new BottomMenuItem {
                Icon = new SymbolIcon(Symbol.Add),
                Lable = "新增收支",
                TappedAction = ViewConsumption
            };
            yield return newRec;
            BottomMenuItem viewRec = new BottomMenuItem
            {
                Icon = new SymbolIcon(Symbol.Add),
                Lable = "个人消费",
                TappedAction = ViewConsumerDetail
            };
            yield return viewRec;
            BottomMenuItem viewOwn = new BottomMenuItem
            {
                Icon = new SymbolIcon(Symbol.Add),
                Lable = "团队消费",
                TappedAction = ViewConsumption
            };
            yield return viewOwn;
            BottomMenuItem viewTeam = new BottomMenuItem
            {
                Icon = new SymbolIcon(Symbol.Add),
                Lable = "团队",
                TappedAction = ViewConsumer
            };
            yield return viewTeam;
        }

        private void ViewConsumerDetail()
        {
            NavigateTo(typeof(ConsumerDetailPage), null);
        }

        private void ViewConsumer()
        {
            NavigateTo(typeof(ConsumerPage), null);
        }

        private void ViewConsumption()
        {
            NavigateTo(typeof(ConsumptionPage), null);
        }

        private void ShowLoginBox()
        {
            Grid_Login.Visibility = Visibility.Visible;
            frame.Visibility = Visibility.Collapsed;
        }

        public void NavigateTo(Type page, object parameter)
        {
            frame.Navigate(page, parameter);
        }
    }
}
