using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace Gank.Bean
{
    public class GankModel: IValueConverter
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Desc { get; set; }

        public DateTime PublishedAt { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public string Used { get; set; }

        public string Who { get; set; }

        public List<string> Images { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Ganhuo_id { get; set; }

        public string Readability { get; set; }

        public String GetWidthUrl(int width)
        {
            return Url + "?imageView2/0/w/" + width;
        }

        public String GetPublishTimeSpan()
        {
            return GetTimSpan(PublishedAt);
        }

        public string GetItemSign()
        {
            return Who + "·" + GetTimSpan(PublishedAt);
        }

        public  string GetTimSpan(DateTime date)
        {
            var now = DateTime.Now;
            if (date.Year != now.Year || date.Month != now.Month)
                return date.ToString("yyyy-MM-dd");
            if (date.Day != now.Day)
                return now.Day - date.Day + "天前";
            if (date.Hour != now.Hour)
                return now.Hour - date.Hour + "小时前";
            if (date.Minute != now.Minute)
                return now.Minute - date.Minute + "分前";
            return now.Second - date.Second + "mn前";
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime date = (DateTime)value;
            return GetTimSpan(date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}