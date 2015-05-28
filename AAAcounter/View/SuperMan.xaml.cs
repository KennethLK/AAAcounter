using AAAcounter.Model;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AAAcounter.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SuperMan : Page
    {

        MainViewController _vc = null;

        public SuperMan()
        {
            this.InitializeComponent();
            Loaded += SuperMan_Loaded;
        }

        private async void SuperMan_Loaded(object sender, RoutedEventArgs e)
        {
            List<ConsumerModel> consumers = await _vc.GetConsumerList();
            listView.ItemsSource = consumers;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _vc = (MainViewController)(e.Parameter);
        }

        private async void btn_commit_Click(object sender, RoutedEventArgs e)
        {
            await _vc.Login(tbx_user.Text, DefaultStrings.DEFAULT_PWD);
        }
    }
}
