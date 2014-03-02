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

        public static void SaveFeedItemsToStorage()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream("TestFile.xml", FileMode.Create,store))
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

                using (var stream = new IsolatedStorageFileStream("TestFile.xml", FileMode.OpenOrCreate, FileAccess.Read, store))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        if (!reader.EndOfStream)
                        {
                            var serializer = new XmlSerializer(typeof(List<FeedItem>));
                            SavedFeedItems.Add((FeedItem)serializer.Deserialize(reader));
                        }
                    }
                }

                return SavedFeedItems;
            }

            

        }
    }
}
