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
            string videosURL = "http://www.youtube.com/rss/user/respectthephoenix/videos.rss";
            string picturesURL = "https://picasaweb.google.com/data/feed/base/user/108546236473040953940/albumid/5778978916894706129?alt=rss&kind=photo&hl=en_US";
            string calendarURL = "http://feeds.feedburner.com/icscalendar";


            var wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(homeStringHandler);
            wc.DownloadStringAsync((new Uri("http://feeds.feedburner.com/google/mUzI")));
            //wc.DownloadStringAsync((new Uri("http://feeds.feedburner.com/TechCrunch/")));

             WebClient wc2 = new WebClient();
            wc2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(videosStringHandler);
            wc2.DownloadStringAsync((new Uri(videosURL)));

            WebClient wc3 = new WebClient();
            wc3.DownloadStringCompleted += new DownloadStringCompletedEventHandler(picturesStringHandler);
            wc3.DownloadStringAsync((new Uri(picturesURL)));

            WebClient wc4 = new WebClient();
            wc4.DownloadStringCompleted += new DownloadStringCompletedEventHandler(calendarStringHandler);
            wc4.DownloadStringAsync((new Uri(calendarURL)));
            
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
                                       //Date = DateTime.Parse(tweet.Element("pubDate").Value)
                                   };
            //homeListBox.ItemsSource = items;
            //homeListBox.UpdateLayout();
            homeProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void videosStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "";
            videosListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                select new RssItem
                {
                    //ImageSource = tweet.Element("media").Attribute("url").Value,
                    Description = tweet.Element("description").Value,
                    Title = tweet.Element("title").Value,
                    Date = DateTime.Parse(tweet.Element("pubDate").Value)
                };
            videosProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void picturesStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "";
            picturesListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                    select new RssItem
                    {
                        //ImageSource = tweet.Element("media").Attribute("url").Value,
                        Description = tweet.Element("description").Value,
                        Title = tweet.Element("title").Value,
                        Date = DateTime.Parse(tweet.Element("pubDate").Value)
                    };
            picturesProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void calendarStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "";
            calendarListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                                      select new RssItem
                                      {
                                          //ImageSource = tweet.Element("media").Attribute("url").Value,
                                          Description = tweet.Element("description").Value,
                                          Title = tweet.Element("title").Value,
                                          //Date = DateTime.Parse(tweet.Element("pubDate").Value)
                                      };
            calendarProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            
        }

        private void ListBoxItemASB_Tap(object sender, GestureEventArgs e)
        {
            //string url = "http://www.google.com";
            string url = "https://sites.google.com/site/icsclubsasb/";
            NavigationService.Navigate(new Uri("/clubs.xaml?url="+url, UriKind.Relative));
        }
    }
}