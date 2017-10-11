using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Gank.Net;

namespace Gank.Bean
{
    public class GankModelCollection : ObservableCollection<GankModel>, ISupportIncrementalLoading
    {
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            page++;
            GetGankTypeDatas();
            return AsyncInfo.Run((c) => GetGankTypeDatas());
        }

        private int page = 1;
        public bool HasMoreItems => true;

        private async Task<LoadMoreItemsResult> GetGankTypeDatas()
        {
            GankTypeDatas datas = await ApiServer.GetGankTypeDatas(ApiServer.TypeFuli, page);
            if (page == 1)
               Clear();
            datas.Results.ForEach(i => Add(i));

            return new LoadMoreItemsResult { Count = (uint)datas.Results.Count };
        }

        public void Refresh()
        {
            page = 1;
            GetGankTypeDatas();
        }
    }
}