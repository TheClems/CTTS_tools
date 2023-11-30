using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Core;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CTTS_Tools
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Dictionary<string, Type> pages = new Dictionary<string, Type>()
        {
            { "home", typeof(HomePage) },
            { "clipboard", typeof(ClipboardPage) },
            { "device", typeof(DevicePage) },
        };

        public MainPage()
        {
            InitializeComponent();
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems[0];
            ContentFrame.Navigate(typeof(HomePage), null);

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // Set active window colors
            titleBar.ForegroundColor = Colors.White;
            titleBar.BackgroundColor = Color.FromArgb(255, 10, 20, 30);
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Color.FromArgb(255, 10, 20, 30);
            titleBar.ButtonHoverForegroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = Colors.RoyalBlue;
            titleBar.ButtonPressedForegroundColor = Colors.White;
            titleBar.ButtonPressedBackgroundColor = Colors.DeepSkyBlue;

            // Set inactive window colors
            titleBar.InactiveForegroundColor = Colors.White;
            titleBar.InactiveBackgroundColor = Colors.Black;
            titleBar.ButtonInactiveForegroundColor = Colors.Gray;
            titleBar.ButtonInactiveBackgroundColor = Colors.Black;
        }

        private void NavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var navItemTag = args.InvokedItemContainer.Tag.ToString();
            NavViewNavigate(navItemTag, args.RecommendedNavigationTransitionInfo);
        }

        private void NavViewNavigate(string tag, NavigationTransitionInfo transitionInfo)
        {
            Type page = null;

            var item = pages.FirstOrDefault(p => p.Key.Equals(tag));

            page = item.Value;

            var previousNavPageType = ContentFrame.CurrentSourcePageType;

            if (page != null && !Equals(previousNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }
    }
}
