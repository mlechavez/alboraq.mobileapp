using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        
        public HomeViewModel()
        {
            GetFeatures();                            
        }        

        private ObservableCollection<Feature> _features;
        public ObservableCollection<Feature> Features
        {
            get { return _features ?? (_features = new ObservableCollection<Feature>()); }
            set
            {
                _features = value;
                OnPropertyChanged("Features");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        void GetFeatures()
        {
            var featureList = new List<Feature>
            {
                new Feature { Title = "Appointment", Detail = "Make an appointment",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" },
                new Feature { Title = "Products", Detail = "Start shopping now!",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png"},
                new Feature { Title = "Porsche Car Configurator", Detail = "Build your car.",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" },
                new Feature { Title = "Menu Packages", Detail = "Packages suited for your car",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" },
                new Feature { Title = "Special Offers", Detail = "Get inside and on our on-going offers!",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" }                
            };

            foreach (var feature in featureList)
            {
                Features.Add(feature);
            }
            
        }
    }
}
