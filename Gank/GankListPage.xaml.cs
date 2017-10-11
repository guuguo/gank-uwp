using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gank.Bean;
using Gank.Helper;
using Gank.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Gank
{
    /// <summary>
    ///     可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GankListPage : Page
    {
        private bool IsClicks = false;
        private readonly GankListViewModel viewModel = new GankListViewModel();

        public GankListPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
            viewModel.DataSource.Refresh();
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
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

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = e.ClickedItem as GankModel;
            NavigationHelper.SendNavigateTo(NavigateMode.Master, typeof(DailyContentPage), info);
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            viewModel.DataSource.Refresh();
        }
    }
}