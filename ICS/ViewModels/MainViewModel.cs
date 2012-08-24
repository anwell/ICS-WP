using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace ICS
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.Items.Add(new ItemViewModel() { LineOne = "Dr. Matthew Livingston", LineTwo = "Principal", LineThree = "mlivingston@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Harriett Peterson", LineTwo = "Office Manager", LineThree = "hpeterson@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Melissa Nelson", LineTwo = "Arts", LineThree = "menelson@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Kathy Comeau", LineTwo = "Arts + Academic Counselor", LineThree = "kcomeau@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Mark Rayder", LineTwo = "Arts", LineThree = "mrayder@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Diana Di-Tolla", LineTwo = "Spanish", LineThree = "dditolla@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Tim McFaul", LineTwo = "Sciences", LineThree = "tmcfaul@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Helen Henry", LineTwo = "Math", LineThree = "hhenry@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Nancy Sullivan", LineTwo = "Math", LineThree = "nasullivan@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Paul Plank", LineTwo = "Humanities + Film Studies", LineThree = "pplank@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Victoria Castaneda", LineTwo = "Humanities", LineThree = "vcastaneda@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Andrya Packer", LineTwo = "Humanities", LineThree = "apacker@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Ted Over", LineTwo = "International Studies", LineThree = "eover@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Mark Elliott", LineTwo = "International Studies + Video Production", LineThree = "melliot@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Mark Bach", LineTwo = "International Studies", LineThree = "mbach@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Nicole Winard", LineTwo = "Librarian", LineThree = "nwinard@lwsd.org" });
            this.Items.Add(new ItemViewModel() { LineOne = "Mary Ajmera", LineTwo = "Para Educator", LineThree = "majemera@lwsd.org" });
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}