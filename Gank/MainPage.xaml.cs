using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using BK20;
using Gank.Bean;
using Gank.Helper;
using Gank.Net;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Gank
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }
        bool IsClicks = false;
        private async void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (FrameDetail.CanGoBack)
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
                    {
                        if (IsClicks)
                        {
                            Application.Current.Exit();
                        }
                        else
                        {
                            IsClicks = true;
                            e.Handled = true;
                            MessShow.Show("再按一次退出应用", 1500);
                            await Task.Delay(1500);
                            IsClicks = false;
                        }
                    }

                }
            }
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            FrameMaster.Navigate(typeof(BlankPage));
            FrameDetail.Navigate(typeof(BlankPage));
            NavigationHelper.MianNavigateToEvent += NavigationHelper_MianNavigateToEvent; ;
            NavigationHelper.DetailNavigateToEvent += NavigationHelperDetailNavigateToEvent; ;
            NavigationHelper.MasterNavigateToEvent += NavigationHelperMasterNavigateToEvent; ;

            GetGankTypeDatas();
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
            this.Frame.Navigate(page, par);

        }

     

      private  async void GetGankTypeDatas()
        {
            GankTypeDatas datas =await ApiServer.GetGankTypeDatas(ApiServer.TypeFuli, 1);
            MasterListView.ItemsSource = datas.Results;
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {

        }


        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
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
          MessShow.Show(FrameMaster.BackStack.Count.ToString(),2000); 
                if (FrameMaster.CanGoBack)
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                }
                else
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

                }

        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = e.ClickedItem as GankModel;
            FrameMaster.Navigate(typeof(DailyContentPage), info,new DrillInNavigationTransitionInfo());
        }

        private void FrameContent_Navigated(object sender, NavigationEventArgs e)
        {
          MessShow.Show(FrameMaster.BackStack.Count.ToString(),2000);
            if (FrameDetail.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            }
        }
    }
}
