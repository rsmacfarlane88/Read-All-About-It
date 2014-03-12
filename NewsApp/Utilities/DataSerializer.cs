using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NewsApp.Utilities
{
    public static class DataSerializer
    {
        public static List<FeedItem> SavedFeedItems;
        public static List<Feed> SavedFeeds;
             
        public static void SaveFeedItemsToStorage()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream("FeedItems.xml", FileMode.Create,store))
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(List<FeedItem>));
                        serializer.Serialize(stream, SavedFeedItems);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    
                }
            }
        }

        public static List<FeedItem> LoadFeedItemsFromStorage(string filename)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                SavedFeedItems = new List<FeedItem>();

                using (var stream = new IsolatedStorageFileStream("FeedItems.xml", FileMode.OpenOrCreate, FileAccess.Read, store))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        if (!reader.EndOfStream)
                        {
                            var serializer = new XmlSerializer(typeof(List<FeedItem>));
                            SavedFeedItems = (List<FeedItem>)serializer.Deserialize(reader);
                        }
                    }
                }

                return SavedFeedItems;
            }
        }

        public static void SaveFeedsToStorage()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream("Feeds.xml", FileMode.Create, store))
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(List<Feed>));
                        serializer.Serialize(stream, SavedFeeds);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }

        public static List<Feed> LoadFeedsFromStorage(string filename)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                SavedFeeds = new List<Feed>();

                using (var stream = new IsolatedStorageFileStream("Feeds.xml", FileMode.OpenOrCreate, FileAccess.Read, store))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        if (!reader.EndOfStream)
                        {
                            var serializer = new XmlSerializer(typeof(List<Feed>));
                            SavedFeeds = (List<Feed>)serializer.Deserialize(reader);
                        }
                    }
                }

                return SavedFeeds;
            }



        }
    }
}
