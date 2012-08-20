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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System.Windows.Navigation;

namespace ICS
{
    public partial class clubs : PhoneApplicationPage
    {
        public clubs()
        {
            InitializeComponent();
            //webBrowser1.LoadCompleted += Browser_dohack;
            
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string url = "";
            if (NavigationContext.QueryString.TryGetValue("url", out url))
                webBrowser1.Navigate(new Uri(url, UriKind.Absolute));
        }

        private void Browser_dohack(object sender, NavigationEventArgs e)
        {
            string html = webBrowser1.SaveToString();
            string hackstring = "<meta name=\"viewport\" content=\"width=50,user-scalable=no\" />";
            html = html.Insert(html.IndexOf("<head>", 0) + 6, hackstring);
            webBrowser1.NavigateToString(html);
            webBrowser1.LoadCompleted -= Browser_dohack;
        }

        private void webBrowser1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            webBrowser1.Opacity = 1;
        }
    }

   
    
}