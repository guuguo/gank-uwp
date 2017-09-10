using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace Gank.Bean
{
    public class GankDailyResult: GankNetResult
    {
        public GankDayliResultDetail results { get; set; }
        public List<String> category { get; set; }
    }
}