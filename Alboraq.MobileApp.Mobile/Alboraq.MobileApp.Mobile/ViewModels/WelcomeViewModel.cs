using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Services;
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
        public WelcomeViewModel()
        {                        
        }

        public INavigation Navigation { get; set; }

        public ICommand GotoSignInPageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var loginPage = new LoginPage();
                    
                    await Navigation.PushAsync(loginPage, true);
                });
            }
        }

        public ICommand GotoRegisterPageCommand
        {
            get
            {
                return new Command(async () => 
                {
                    var registerPage = new RegisterPage();

                    await Navigation.PushAsync(registerPage, true);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
