using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace NewsApp.Utilities
{
    public class FeedDataAccess
    {
        public List<Feed> GetDefaultHeadlineFeeds()
        {
            return DataSerializer.LoadFeedsFromStorage("test");
        }

        public void CreateDefaultHeadlineFeeds()
        {
            List<Feed> defaultFeeds = new List<Feed>()
            {
                new Feed{Category = FeedCategory.News,Name="BBC Headlines",FeedUri="http://feeds.bbci.co.uk/news/rss.xml",Publisher="BBC News"},
                new Feed{Category=FeedCategory.News, Name="Bing Top Stories",FeedUri="http://www.bing.com/news/?format=RSS", Publisher="Bing"},
                new Feed{Category=FeedCategory.News, Name="Daily Mail Headlines",FeedUri="http://www.dailymail.co.uk/home/index.rss", Publisher="Daily Mail"}
            };

            DataSerializer.SavedFeeds = defaultFeeds;
            DataSerializer.SaveFeedsToStorage();
        }

        
    }
}
