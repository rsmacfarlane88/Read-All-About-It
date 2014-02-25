using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri ImageUri { get; set; }
        public Uri ItemLink { get; set; }
    }
}
