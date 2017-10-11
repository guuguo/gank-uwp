using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gank.Bean;
using Gank.Helper;
using Gank.Net;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Gank
{
    /// <summary>
    ///     可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DailyContentPage : Page
    {
        ObservableCollection<GroupModel> _gankGroups =new ObservableCollection<GroupModel>();
        public DailyContentPage()
        {
            InitializeComponent();
            GankGroupModels.Source = _gankGroups;
        }

        public GankModel _gankModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _gankModel = (GankModel)((object[]) e.Parameter)[0];
             GetGankDailyDate();
        }

        private async void GetGankDailyDate()
        {
            var datas = await ApiServer.GetGankDailyDate(_gankModel.PublishedAt);

//            var groupDates = new List<GroupModel>();
            AddGroup(_gankGroups, datas.results.休息视频);
            AddGroup(_gankGroups, datas.results.Android);
            AddGroup(_gankGroups, datas.results.iOS);
            AddGroup(_gankGroups, datas.results.前端);

        }

        private void AddGroup(ObservableCollection<GroupModel> groupDates, List<GankModel> items)
        {
            if(items!=null && items.Count>0)
                groupDates.Add(new GroupModel(items[0].Type, items));
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = e.ClickedItem as GankModel;
            NavigationHelper.SendNavigateTo(NavigateMode.Main, typeof(WebViewPage), info,true);
            NavigationHelper.SendNavigateTo(NavigateMode.Detail, typeof(WebViewPage), info,false);
        }

    }
}