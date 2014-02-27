using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NewsApp.Resources;
using System.Collections.ObjectModel;
using System.Xml;
//using System.ServiceModel.Syndication;
using System.Xml.Linq;
using System.Net.NetworkInformation;

namespace NewsApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ObservableCollection<FeedItem> HeadlineItems;
        private ObservableCollection<FeedItem> MostReadItems;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                FillHeadlines();
                FillMostRead();
            }

            
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void FillHeadlines()
        {
            HeadlineItems = new ObservableCollection<FeedItem>();
            ListBoxHeadlines.ItemsSource = HeadlineItems;

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += wc_HeadlinesDownloadCompleted;
            wc.DownloadStringAsync(new Uri("http://feeds.bbci.co.uk/news/mobile/rss.xml?edition=uk"));
            
        }

        private void FillMostRead()
        {
            MostReadItems = new ObservableCollection<FeedItem>();
            ListBoxMostRead.ItemsSource = MostReadItems;

            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += wc_MostReadDownloadCompleted;
            wc.DownloadStringAsync(new Uri("http://feeds.bbci.co.uk/rss/newsonline_uk_edition/livestats/most_emailed/rss.xml"));
        }

        private void wc_MostReadDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var media = XNamespace.Get("http://search.yahoo.com/mrss/");
            var result = e.Result;

            XElement items = XElement.Parse(result);
            var mostRead = items.Descendants("item")
                   .Select(item => new FeedItem
                   {
                       Title = item.Element("title").Value,
                       Description = item.Element("description").Value,
                       //ImageUri = new Uri(item.Element(media + "thumbnail").Attribute("url").Value),//Where(i => i.Attribute("width").Value == "144" && i.Attribute("height").Value == "81").Select(i => i.Attribute("url").Value).SingleOrDefault())
                       ItemLink = new Uri(item.Element("link").Value)
                   }).Take(5);

            foreach (var item in mostRead)
            {
                MostReadItems.Add(item);
            }
        }

        void wc_HeadlinesDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var media = XNamespace.Get("http://search.yahoo.com/mrss/");
            var result = e.Result;

            XElement items = XElement.Parse(result);
            var headlines = items.Descendants("item")
                   .Select(item => new FeedItem
                   {
                       Title = item.Element("title").Value,
                       Description = item.Element("description").Value,
                       ImageUri = item.Element(media+"thumbnail") == null ? null: new Uri(item.Element(media+"thumbnail").Attribute("url").Value),//Where(i => i.Attribute("width").Value == "144" && i.Attribute("height").Value == "81").Select(i => i.Attribute("url").Value).SingleOrDefault())
                       ItemLink = new Uri(item.Element("link").Value)
                   });

            foreach (var item in headlines)
            {
                HeadlineItems.Add(item);
            }

            
        }

        private void ListBoxHeadlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (FeedItem)ListBoxHeadlines.SelectedItem;
            
            if (selectedItem != null)
            {
                NavigationService.Navigate(new Uri(string.Format("/Views/ItemDetail.xaml?link={0}", selectedItem.ItemLink), UriKind.Relative));
            }

            ListBoxHeadlines.SelectedItem = null;
        }

        private void ListBoxMostRead_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (FeedItem)ListBoxMostRead.SelectedItem;

            if (selectedItem != null)
            {
                NavigationService.Navigate(new Uri(string.Format("/Views/ItemDetail.xaml?link={0}", selectedItem.ItemLink), UriKind.Relative));
            }

            ListBoxMostRead.SelectedItem = null;
        }

        private void ListBoxSections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ReadLater_Click(object sender, RoutedEventArgs e)
        {
            var selected = (sender as MenuItem).DataContext as FeedItem;
            var title = selected.Title;
            
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //appBarButton.Text = AppResources.AppBarButtonText;
            //ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem settingsMenuItem = new ApplicationBarMenuItem(AppResources.SettingsMenuItemText);
            settingsMenuItem.Click += settingsMenuItem_Click;
            ApplicationBar.MenuItems.Add(settingsMenuItem);
        }

        void settingsMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Settings.xaml", UriKind.Relative));
        }
    }
}