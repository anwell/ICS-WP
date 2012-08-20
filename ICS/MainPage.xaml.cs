using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Phone.Controls;

namespace ICS
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

            //WebClient.

            var wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(homeStringHandler);
            //wc.DownloadStringAsync((new Uri("http://feeds.feedburner.com/TechCrunch/")));
        }

        private void homeStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            //XNamespace ns = "urlhere.com///";
            homeListBox.ItemsSource = 
            from tweet in xDoc.Descendants("item")
                                   select new RssItem
                                   {
                                       //ImageSource = tweet.Element("media").Attribute("url").Value,
                                       Description = tweet.Element("description").Value,
                                       Title = tweet.Element("title").Value,
                                       Date = DateTime.Parse(tweet.Element("pubDate").Value)
                                   };
            //homeListBox.ItemsSource = items;
            //homeListBox.UpdateLayout();
        }

        private void videosStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "urlhere.com///";
            homeListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                select new RssItem
                {
                    ImageSource = tweet.Element("media").Attribute("url").Value,
                    Description = tweet.Element("description").Value,
                    Title = tweet.Element("title").Value,
                    Date = DateTime.Parse(tweet.Element("pubDate").Value)
                };
        }

        private void picturesStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "urlhere.com///";
            homeListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                    select new RssItem
                    {
                        ImageSource = tweet.Element("media").Attribute("url").Value,
                        Description = tweet.Element("description").Value,
                        Title = tweet.Element("title").Value,
                        Date = DateTime.Parse(tweet.Element("pubDate").Value)
                    };
        }
        private void calendarStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "urlhere.com///";
            homeListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                                      select new RssItem
                                      {
                                          //ImageSource = tweet.Element("media").Attribute("url").Value,
                                          Description = tweet.Element("description").Value,
                                          Title = tweet.Element("title").Value,
                                          Date = DateTime.Parse(tweet.Element("pubDate").Value)
                                      };
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            
        }

        private void ListBoxItem_Tap(object sender, GestureEventArgs e)
        {
            //string url = "http://www.google.com";
            string url = "https://sites.google.com/site/mobileblankblack/";
            NavigationService.Navigate(new Uri("/clubs.xaml?url="+url, UriKind.Relative));
        }
    }
}