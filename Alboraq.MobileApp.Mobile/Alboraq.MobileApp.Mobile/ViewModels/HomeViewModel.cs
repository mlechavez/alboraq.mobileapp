using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private List<Feature> _features;
        public List<Feature> Features
        {
            get { return _features ?? (_features = new List<Feature>()); }
            set
            {
                _features = value;
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

            Features.AddRange(featureList);
        }
    }
}
