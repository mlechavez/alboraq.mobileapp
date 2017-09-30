using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class WelcomeViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public WelcomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;          
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

        public ICommand GotoSignInPageCommand
        {
            get { return new Command(() => _navigationService.PushAsync(App.LoginPage)); }
        }

        public ICommand GotoRegisterPageCommand
        {
            get { return new Command(() => _navigationService.PushAsync(App.RegisterPage)); }
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
                new Feature { Title = "Products", Detail = "A variety of products are available to choose from!" },
                new Feature { Title = "Menu Packages", Detail = "Price list of services based on the model of your car" },
                new Feature { Title = "Menu Packages", Detail = "Price list of services based on the model of your car" },
                new Feature { Title = "Menu Packages", Detail = "Price list of services based on the model of your car" }
            };

            Features.AddRange(featureList);
        }
    }
}
