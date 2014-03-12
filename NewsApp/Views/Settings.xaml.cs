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
        AppSettings settings;
        public Settings()
        {
            InitializeComponent();
            settings = new AppSettings();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            FeedDataAccess fda = new FeedDataAccess();
            //fda.CreateDefaultHeadlineFeeds();
            lpHomeNewsFeed.ItemsSource = fda.GetDefaultHeadlineFeeds();
        }

        private void lpHomeNewsFeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (Feed)lpHomeNewsFeed.SelectedItem;
            
            if(selected != null)
                settings.HomeNewsFeedSetting = selected; 
        }

    }
}