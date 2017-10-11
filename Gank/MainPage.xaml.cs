using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Gank.Bean;
using Gank.Helper;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Gank
{
    /// <summary>
    ///     可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool _IsClicks;
        private bool _isNarrow;

        private Type _lastDetailFramePageType;
        private object[] _lastDetailFramepar;

        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            NavigationHelper.MianNavigateToEvent += NavigationHelper_MianNavigateToEvent;
            ;
            NavigationHelper.DetailNavigateToEvent += NavigationHelperDetailNavigateToEvent;
            ;
            NavigationHelper.MasterNavigateToEvent += NavigationHelperMasterNavigateToEvent;

            NavigationHelper.SendNavigateTo(NavigateMode.Detail, typeof(DetailBlankPage));
            NavigationHelper.SendNavigateTo(NavigateMode.Master, typeof(GankListPage));
        }

        private async void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
                FrameDetail.GoBack();
            }
            else if (FrameDetail.CanGoBack)
            {
                e.Handled = true;
                FrameDetail.GoBack();
            }
            else
            {
                if (FrameMaster.CanGoBack)
                {
                    e.Handled = true;
                    FrameMaster.GoBack();
                }
                else
                {
                    if (e.Handled == false)
                        if (_IsClicks)
                        {
                            Application.Current.Exit();
                        }
                        else
                        {
                            _IsClicks = true;
                            e.Handled = true;
                            MessShow.Show("再按一次退出应用", 1500);
                            await Task.Delay(1500);
                            _IsClicks = false;
                        }
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateForVisualState(AdaptiveStates.CurrentState);
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void NavigationHelperDetailNavigateToEvent(Type page, params object[] par)
        {
            FrameDetail.Navigate(page, par);
        }

        private void NavigationHelperMasterNavigateToEvent(Type page, params object[] par)
        {
            FrameMaster.Navigate(page, par);
        }

        private void NavigationHelper_MianNavigateToEvent(Type page, params object[] par)
        {
            Frame.Navigate(page, par);
            _lastDetailFramePageType = page;
            _lastDetailFramepar = par;
        }


        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            _isNarrow = newState == NarrowState;
            if (_isNarrow && FrameDetail.CanGoBack)
                NavigationHelper.SendNavigateTo(NavigateMode.Main, _lastDetailFramePageType, _lastDetailFramepar);
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(0xFF, 0x3B, 0x3C, 0x41);
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Color.FromArgb(0xFF, 0x3B, 0x3C, 0x41);
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = Colors.DodgerBlue;
            titleBar.ButtonPressedBackgroundColor = Colors.OrangeRed;
            titleBar.ButtonHoverForegroundColor = Colors.White;
            titleBar.ButtonPressedForegroundColor = Colors.White;


            // inactive
            titleBar.InactiveBackgroundColor = Color.FromArgb(0xFF, 0x2B, 0x1C, 0x41);
            titleBar.InactiveForegroundColor = Colors.DarkGray;
            titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(0xFF, 0x2B, 0x1C, 0x41);
            titleBar.ButtonInactiveForegroundColor = Colors.DarkGray;
        }

        private void FrameNavigation_Navigated(object sender, NavigationEventArgs e)
        {
            CheckBackVisible();
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = e.ClickedItem as GankModel;
            FrameMaster.Navigate(typeof(DailyContentPage), info, new DrillInNavigationTransitionInfo());
        }

        private void FrameContent_Navigated(object sender, NavigationEventArgs e)
        {
            CheckBackVisible();
        }

        private void CheckBackVisible()
        {
            if (FrameMaster.CanGoBack || FrameDetail.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            else
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
        }
    }
}