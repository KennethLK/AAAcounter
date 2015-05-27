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
    public sealed partial class AA : Page
    {
        AAViewController _viewController = null;

        public AA()
        {
            this.InitializeComponent();

            Loaded += AA_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MainViewController mvc = (MainViewController)e.Parameter;
            if (_viewController == null)
                _viewController = new AAViewController(this);

            _viewController.Initialize(mvc);
        }

        private void AA_Loaded(object sender, RoutedEventArgs e)
        {
            _viewController = new AAViewController(this);
        }
    }
}
