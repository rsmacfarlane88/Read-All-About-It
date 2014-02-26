using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace NewsApp
{
    public partial class ItemDetail : PhoneApplicationPage
    {
        private Uri link;

        public ItemDetail()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            link = new Uri("http://mobilizer.instapaper.com/m?u=" + this.NavigationContext.QueryString["link"]);
            //link = link.Replace("/news/", "/textbased/news/").Replace("/article-", "/text-");

            this.WebBrowser1.Loaded += webBrowser_Loaded;
        }

        void webBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            this.WebBrowser1.Navigate(link);
        }

        private void Refresh_Button_Click(object sender, EventArgs e)
        {
            WebBrowser1.Navigate(new Uri(WebBrowser1.Source.AbsoluteUri));
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            if (WebBrowser1.CanGoBack)
                WebBrowser1.GoBack();
        }

        private void Forward_Button_Click(object sender, EventArgs e)
        {
            if (WebBrowser1.CanGoForward)
                WebBrowser1.GoForward();
        }

        private void Share_Button_Click(object sender, EventArgs e)
        {
            ShareLinkTask share = new ShareLinkTask();
            share.LinkUri = link;
            share.Show();
        }
      
    }
}