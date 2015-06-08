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

namespace AAAccounter.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConsumptionPage : Page
    {
        MainViewController _vc = null;
        public ConsumptionPage()
        {
            this.InitializeComponent();
            Loaded += ConsumptionPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vc = e.Parameter as MainViewController;
            base.OnNavigatedTo(e);
        }

        private async void ConsumptionPage_Loaded(object sender, RoutedEventArgs e)
        {
            var list = await _vc.GetConsumptionList();
            grid_consumption.ItemsSource = list;
        }
    }
}
