using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Globalization;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyToolkit.Controls;
using MyToolkit.Paging;
using MyToolkit.Paging.Animations;
using MyToolkit.UI;
using PainLogUWP.Views;

namespace PainLogUWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : MtApplication
    {
        public override Type StartPageType => typeof(MainPage);
        private HamburgerFrameBuilder _hamburgerFrameBuilder;

        public override FrameworkElement CreateWindowContentElement()
        {
            _hamburgerFrameBuilder = new HamburgerFrameBuilder();

            //var searchItem = new SearchHamburgerItem
            //{
            //    PlaceholderText = "Search",
            //};
            //searchItem.QuerySubmitted += async (sender, args) =>
            //{
            //    await _hamburgerFrameBuilder.Frame.NavigateToExistingOrNewPageAsync(typeof(DataGridPage));
            //    var dataGridPage = (DataGridPage)_hamburgerFrameBuilder.Frame.CurrentPage.Page;
            //    dataGridPage.Model.Filter = args.QueryText;
            //};

           // _hamburgerFrameBuilder.Hamburger.Header = new HamburgerHeader();
            _hamburgerFrameBuilder.Hamburger.TopItems = new ObservableCollection<HamburgerItem>
            {
                new PageHamburgerItem
                {
                    Content = "Main",
                    ContentIcon = new SymbolIcon(Symbol.Home),
                    Icon = new SymbolIcon(Symbol.Home),
                    PageType = typeof(MainPage)
                },
           //     searchItem,
                new PageHamburgerItem
                {
                    Content = "Pain",
                    ContentIcon = new SymbolIcon(Symbol.Clock),
                    Icon = new SymbolIcon(Symbol.Clock),
                    PageType = typeof(PainPage)
                }
            };
            _hamburgerFrameBuilder.Hamburger.BottomItems = new ObservableCollection<HamburgerItem>
            {
                new PageHamburgerItem
                {
                    Content = "Settings",
                    ContentIcon = new SymbolIcon(Symbol.Setting),
                    Icon = new SymbolIcon(Symbol.Setting),
                    PageType = typeof(SettingsPage)
                }
            };
            _hamburgerFrameBuilder.Frame.PageAnimation = new ScalePageTransition();
            return _hamburgerFrameBuilder.Hamburger;
        }

        public override MtFrame GetFrame(UIElement windowContentElement)
        {
            return _hamburgerFrameBuilder.Frame;
        }

        public override async Task OnInitializedAsync(MtFrame frame, ApplicationExecutionState args)
        {
            //await HideStatusBarAsync();
        }

        private async Task HideStatusBarAsync()
        {
            //if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    var statusBar = StatusBar.GetForCurrentView();
            //    await statusBar.HideAsync();
            //}
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            ApplicationViewUtilities.ConnectRootElementSizeToVisibleBounds();
            ApplicationLanguages.PrimaryLanguageOverride = GlobalizationPreferences.Languages[0];
        }
    }
}
