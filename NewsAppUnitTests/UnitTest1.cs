﻿using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using NewsApp.Utilities;

namespace NewsAppUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetHeadlines()
        {
            FeedDataAccess da = new FeedDataAccess();
            da.GetFeed("http://feeds.bbci.co.uk/news/rss.xml");
        }
    }
}
