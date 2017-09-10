using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gank.Bean;
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

        public GankModel GankModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
                GankModel = (GankModel) e.Parameter;
            GetGankDailyDate();
        }

        private async void GetGankDailyDate()
        {
            var datas = await ApiServer.GetGankDailyDate(GankModel.PublishedAt);

//            var groupDates = new List<GroupModel>();
            AddGroup(_gankGroups, datas.results.休息视频);
            AddGroup(_gankGroups, datas.results.Android);
            AddGroup(_gankGroups, datas.results.iOS);
            AddGroup(_gankGroups, datas.results.前端);

//                        GankGroupModels.Source=groupDates;
//                        ListView.ItemsSource = GankGroupModels;
//            _gankGroups.Add();
        }

        private void AddGroup(ObservableCollection<GroupModel> groupDates, List<GankModel> items)
        {
            if(items!=null && items.Count>0)
                groupDates.Add(new GroupModel(items[0].Type, items));
        }
    }
}