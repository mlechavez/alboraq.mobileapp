using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        
        public HomeViewModel()
        {
            GetFeatures();                            
        }
        public INavigation Navigation { get; set; }
        public Page Page { get; set; }

        private ObservableCollection<FeatureModel> _features;

        public ObservableCollection<FeatureModel> Features
        {
            get { return _features ?? (_features = new ObservableCollection<FeatureModel>()); }
            set
            {
                _features = value;
                OnPropertyChanged("Features");
            }
        }

        private FeatureModel _feature;

        public FeatureModel Feature
        {
            get { return _feature; }
            set {
                if(_feature != value)
                {
                    _feature = value;
                    OnPropertyChanged("Feature");
                    GoToPage(_feature.Title);
                    _feature = null;
                }                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand Get { get; set; }

        void GetFeatures()
        {
            var featureList = new List<FeatureModel>
            {
                new FeatureModel { Title = "Appointment", Detail = "Make an appointment",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" },
                new FeatureModel { Title = "Products", Detail = "Start shopping now!",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png"},                
                new FeatureModel { Title = "Menu Packages", Detail = "Packages suited for your car",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" },
                new FeatureModel { Title = "Special Offers", Detail = "Get inside and on our on-going offers!",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" },
                new FeatureModel { Title = "Porsche Car Configurator", Detail = "Build your car.",
                    ImageUrl ="https://xamarin.com/content/images/pages/forms/example-app.png" }
            };

            foreach (var feature in featureList)
            {
                Features.Add(feature);
            }
            
        }

        void GoToPage(string featureName)
        {
            switch (featureName.ToLower())
            {
                case "appointment":
                    var appointmentPage = new AppointmentPage { Title = "Appointment" };
                    Navigation.PushAsync(appointmentPage, animated: true);
                    break;
                case "products":
                    var productCategoryListPage = new ProductCategoryListPage { Title = "Product Categories" };
                    Navigation.PushAsync(productCategoryListPage, animated: true);
                    break;
                case "menu packages":
                    var menupackagesPage = new MenuPackagesPage { Title = "Menu Packages" };
                    Navigation.PushAsync(menupackagesPage);
                    break;
                case "special offers":
                    var specialOffersPage = new SpecialOffersPage { Title = "Special Offers" };
                    Navigation.PushAsync(specialOffersPage);
                    break;
                default:
                    break;
            }
        }
    }
}
