using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gank.Bean;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Gank
{
    /// <summary>
    ///     可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WebViewPage : Page
    {
        private GankModel gankModel;
        private bool isMain = true;

        public WebViewPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ProgressBar.Visibility = Visibility.Visible;
            var objs = (object[]) e.Parameter;
            gankModel = (GankModel) objs[0];
            if (objs.Length > 1)
                isMain = (bool) objs[1];
            WebView.Source = new Uri(gankModel.Url);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ShouldGoToWideState();
        }


        private void OnBackRequested()
        {
            // Page above us will be our master view.
            // Make sure we are using the "drill out" animation in this transition.
        }

        private void NavigateBackForWideState(bool useTransition)
        {
            // Evict this page from the cache as we may not need it again.
            try
            {
                Frame.GoBack();
            }
            catch (Exception e)
            {
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShouldGoToWideState();
        }

        private void ShouldGoToWideState()
        {
            if (isMain && Window.Current.Bounds.Width >= 800)
                NavigateBackForWideState(false);
        }

        private void WebView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            ProgressBar.Visibility=Visibility.Collapsed;
        }
    }
}