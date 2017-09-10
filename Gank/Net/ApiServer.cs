using System;
using System.Threading.Tasks;
using Gank.Bean;
using Gank.Helper;
using Newtonsoft.Json;

namespace Gank.Net
{
    public class ApiServer
    {
        public const string TypeFuli = "福利";
        public const string TypeAndroid = "Android";
        public const string TypeIos = "iOS";
        public const string TypeRest = "休息视频";
        public const string TypeExpand = "拓展资源";
        public const string TypeFront = "前端";
        public const string TypeAll = "all";

        public const int MeiziCount = 20;

        public static async Task<GankTypeDatas> GetGankTypeDatas(string type, int page)
        {
            var uri = Constant.GANK_BASE_API + "data/" + type + "/" + MeiziCount + "/" + page;
            var results = await WebClientClass.GetResults(new Uri(uri));
            var info = JsonConvert.DeserializeObject<GankTypeDatas>(results);
            return info;
        }
        public static async Task<GankDailyResult> GetGankDailyDate(DateTime date)
        {

            var uri = Constant.GANK_BASE_API + "day/" + date.ToString("yyyy/MM/dd");
            var results = await WebClientClass.GetResults(new Uri(uri));
            var info = JsonConvert.DeserializeObject<GankDailyResult>(results);
            return info;
        }
    }
}