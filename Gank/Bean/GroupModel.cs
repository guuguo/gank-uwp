using System;
using System.Collections.Generic;

namespace Gank.Bean
{
    public class  GroupModel : List<Object>
    {
        public   GroupModel(string groupKey, List<GankModel> items)
        {
            items.ForEach(i =>Add(i));
            GroupKey = groupKey;
        }

        public string GroupKey { get; set; }
    }
}