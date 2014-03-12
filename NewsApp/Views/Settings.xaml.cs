using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NewsApp.Utilities;

namespace NewsApp
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            FeedDataAccess fda = new FeedDataAccess();
            //fda.CreateDefaultHeadlineFeeds();
            lpHomeNewsFeed.ItemsSource = fda.GetDefaultHeadlineFeeds();
        }

        private void lpHomeNewsFeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}