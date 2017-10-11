using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gank.Bean;

namespace Gank.ViewModels
{
    public class GankListViewModel
    {
            public GankModelCollection DataSource { get; set; }

            public GankListViewModel()
            {
                // 初始化資料集合
                DataSource = new GankModelCollection();
            }
    }
}
