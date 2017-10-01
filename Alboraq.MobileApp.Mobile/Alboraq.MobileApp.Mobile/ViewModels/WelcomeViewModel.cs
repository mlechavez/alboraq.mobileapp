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
       
    }
}
