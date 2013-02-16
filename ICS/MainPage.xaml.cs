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
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Net.NetworkInformation;

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

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                // creating WebClients to download XML data and trigger an event handler
                WebClient homeWebClient = new WebClient();
                homeWebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(homeStringHandler);
                homeWebClient.DownloadStringAsync((new Uri("http://feeds.feedburner.com/google/mUzI")));

                // If the app subscribed to ICS's Youtube videos.
                // WebClient wc2 = new WebClient();
                //wc2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(videosStringHandler);
                //wc2.DownloadStringAsync((new Uri("http://www.youtube.com/rss/user/respectthephoenix/videos.rss")));

                WebClient picturesWebClient = new WebClient();
                picturesWebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(picturesStringHandler);
                picturesWebClient.DownloadStringAsync((new Uri("http://feeds.feedburner.com/icspictures")));

                WebClient calendarWebClient = new WebClient();
                calendarWebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(calendarStringHandler);
                calendarWebClient.DownloadStringAsync((new Uri("http://feeds.feedburner.com/icscalendar")));
            }
            else
            {
                internetText.Visibility = System.Windows.Visibility.Visible;
                calendarProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                homeProgressBar.Visibility = System.Windows.Visibility.Collapsed;
                picturesProgressBar.Visibility = System.Windows.Visibility.Collapsed;
            }
            
        }

        //upon finishing download, parse the XML
        private void homeStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            //XNamespace ns = "http://search.yahoo.com/mrss/";
            homeListBox.ItemsSource = 
            from tweet in xDoc.Descendants("item")
                                   select new RssItem
                                   {
                                       //ImageSource = tweet.Element("media").Attribute("url").Value,
                                       Description = StripTagsCharArray( tweet.Element("description").Value),
                                       Title = tweet.Element("title").Value,
                                       //Date = DateTime.Parse(tweet.Element("pubDate").Value)
                                   };
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
                    //Description = StripTagsCharArray( tweet.Element("description").Value),
                    Title = tweet.Element("title").Value,
                    //Date = DateTime.Parse(tweet.Element("pubDate").Value)
                };
            videosProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void picturesStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "http://search.yahoo.com/mrss/";
            picturesListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                    select new RssItem
                    {
                        ImageSource = tweet.Element("enclosure").Attribute("url").Value,
                        Description = tweet.Element("description").Value,
                        Title = tweet.Element("title").Value,
                        Link = tweet.Element("link").Value,
                        //Date = DateTime.Parse(tweet.Element("pubDate").Value)
                    };
            picturesProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void calendarStringHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            XElement xDoc = XElement.Parse(e.Result);
            XNamespace ns = "http://purl.org/dc/elements/1.1/";
            calendarListBox.ItemsSource = from tweet in xDoc.Descendants("item")
                                      select new RssItem
                                      {
                                          //ImageSource = tweet.Element("media").Attribute("url").Value,
                                          Description = HttpUtility.HtmlDecode(StripTagsCharArray( tweet.Element("description").Value)),
                                          Title = tweet.Element("title").Value,
                                          //Date = DateTime.Parse(tweet.Element("pubDate").Value)
                                          //Description = tweet.Element("pubDate").Value
                                      };
            calendarProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        //remove html tags from text
        public static string StripTagsCharArray(string source)
        {
	        char[] array = new char[source.Length];
	        int arrayIndex = 0;
	        bool inside = false;

	        for (int i = 0; i < source.Length; i++)
	        {
	            char let = source[i];
	            if (let == '<')
	            {
		        inside = true;
		        continue;
	            }
	            if (let == '>')
	            {
		        inside = false;
		        continue;
	            }
	            if (!inside)
	            {
		        array[arrayIndex] = let;
		        arrayIndex++;
	            }
	        }
	        return new string(array, 0, arrayIndex);
            }
        

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            
        }

        private void ListBoxItemASB_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //string url = "http://www.google.com";
            string url = "https://sites.google.com/site/icsclubsasb/";
            NavigationService.Navigate(new Uri("/clubs.xaml?url="+url, UriKind.Relative));
        }

        private void ListBoxItemFBLA_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/fbla";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemKey_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/keyclub";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemMock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/mocktrial";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemMUN_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/mun";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemNHS_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/nhs";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemNAHS_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/nahs";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemSNHS_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/snhs";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemEnsemble_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/orchestra";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemChoir_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/choir";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemTaste_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/taste";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemEnvironmental_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/environmentalclub";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemUltimate_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/ultimatefrisbee";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemTech_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/techcrew";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBoxItemDrama_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string url = "https://sites.google.com/site/icsclubsasb/drama";
            NavigationService.Navigate(new Uri("/clubs.xaml?url=" + url, UriKind.Relative));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selection = (ItemViewModel)e.AddedItems[0];
                

                EmailComposeTask emailcomposer = new EmailComposeTask();
                emailcomposer.To = selection.LineThree;
                emailcomposer.Show();
            }
        }

        private void picturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selection = (RssItem)e.AddedItems[0];
                NavigationService.Navigate(new Uri("/clubs.xaml?url=" + selection.ImageSource, UriKind.Relative));
            }
        }
    }
}