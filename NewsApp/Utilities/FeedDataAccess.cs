using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


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

        public ObservableCollection<FeedItem> GetFeed(string feedXml)
        {
            ObservableCollection<FeedItem> feedItems = new ObservableCollection<FeedItem>();

            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.DtdProcessing = DtdProcessing.Parse;

            XmlReader reader = XmlReader.Create(new StringReader(feedXml), xmlSettings);
            var media = XNamespace.Get("http://search.yahoo.com/mrss/");
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (var item in feed.Items)
            {
                FeedItem feedItem = new FeedItem();
                feedItem.Title = Regex.Replace(item.Title.Text, @"\t|\n|\r", "");
                feedItem.Publisher = feed.Title.Text;
                var desc = Regex.Replace(item.Summary.Text, @"\t|\n|\r", "");
                feedItem.Description = desc.Length > 250 ? desc.Substring(0, 245) + "..." : desc;
                feedItem.ItemLink = item.Links[0].Uri.ToString();

                foreach (var extension in item.ElementExtensions)
                {
                    XElement ele = extension.GetObject<XElement>();
                    var xAttribute = ele.Attribute("url");
                    if (xAttribute != null) feedItem.ImageUri = xAttribute.Value;
                }


                feedItems.Add(feedItem);
            }

            reader.Close();

            return feedItems;
        }

     
    }
}
