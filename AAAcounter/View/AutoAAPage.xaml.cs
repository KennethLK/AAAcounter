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
    public sealed partial class AutoAAPage : Page
    {
        MainViewController _vc = null;
        public AutoAAPage()
        {
            this.InitializeComponent();
            Loaded += AutoAAPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vc = e.Parameter as MainViewController;
            base.OnNavigatedTo(e);
        }

        private async void AutoAAPage_Loaded(object sender, RoutedEventArgs e)
        {
            tb_place.ItemsSource = new string[] {
                "C8食堂点菜",
                "C8食堂套餐",
                "麦当劳",
                "谢记燃面",
                "豌豆面",
                "D7食堂",
                "冰冰稀饭庄",
            };

            listView_people.ItemsSource = await _vc.GetConsumerList();
        }

        private async void btn_save_Click(object sender, RoutedEventArgs e)
        {
            List<ConsumerModel> consumers = listView_people.SelectedItems.Cast<ConsumerModel>().ToList();
            Consumption con = new Consumption
            {
                CreateDate = DateTime.Now.GetMili1970(),
                Money = double.Parse(tb_money.Text),
                PeopleCount = consumers.Count,
                Place = tb_place.Text,
                PlaceType = PlaceType.Food,
            };
            List<ConsumptionItemList> citems = new List<ConsumptionItemList>();
            var items = tb_list.Text.Split(' ');
            await _vc.AddConsumption(con);
            foreach (var item in items)
            {
                citems.Add(new ConsumptionItemList
                {
                    Item = item,
                    Price = 0,
                    ConsumptionId = con.Id,
                });
            }

            await _vc.SaveConsumptionItems(citems);

            try
            {
                _vc.NavigateTo(typeof(AAPage));
            }
            catch
            {
                throw;
            }
            //await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            //{
            //    try
            //    {
            //        _vc.NavigateTo(typeof(Consumption));
            //    }
            //    catch
            //    {
            //        throw;
            //    }
            //});
        }
    }
}
