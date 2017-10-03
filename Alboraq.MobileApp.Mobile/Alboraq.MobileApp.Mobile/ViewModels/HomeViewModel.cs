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
            
            BlobCache.Secure.GetObject<AppCredentials>("login")
                .Subscribe(x => LoginTest = x, exs => Debug.WriteLine("no key!"));            
        }

        private AppCredentials _login;

        public AppCredentials LoginTest
        {
            get { return _login; }
            set { _login = value;
                OnPropertyChanged("LoginTest");
            }
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
                new Feature { Title = "Appointment", Detail = "Set a service appointment for your car and receive a confirmation" },
                new Feature { Title = "Menu Packages", Detail = "Price list of services based on the model of your car" },
                new Feature { Title = "Special Offers", Detail = "Get inside and on our on-going offers!" },                
                new Feature { Title = "Porsche Car Configurator", Detail = "Start building your car here." },
                new Feature { Title = "Products", Detail = "Start shopping now!" }
            };

            foreach (var feature in featureList)
            {
                Features.Add(feature);
            }
            
        }
    }
}
