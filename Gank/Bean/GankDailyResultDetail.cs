using System.Collections.Generic;

namespace Gank.Bean
{
    public class GankDayliResultDetail
    {
        public long date { get; set; }


        public List<GankModel> Android { get; set; }

        public List<GankModel> APP { get; set; }

        public List<GankModel> iOS { get; set; }


//        @SerializedName("前端")
        public List<GankModel> 前端 { get; set; }

        //        @SerializedName("休息视频")
        public List<GankModel> 休息视频 { get; set; }

        //        @SerializedName("拓展资源")
        public List<GankModel> 拓展资源 { get; set; }

        //        @SerializedName("瞎推荐")
        public List<GankModel> 瞎推荐 { get; set; }

        //        @SerializedName("福利")
        public List<GankModel> 福利 { get; set; }
    }
}