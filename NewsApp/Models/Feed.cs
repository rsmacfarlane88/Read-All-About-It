using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp
{
    public class Feed
    {
        public string Name { get; set; }
        public string FeedUri { get; set; }
        public FeedCategory Category { get; set; }
        public string Publisher { get; set; }

    }

    public enum FeedCategory
    {
        News,
        Sport,
        Programming,
        Finance,
        Technology,
        Other
    }
}
