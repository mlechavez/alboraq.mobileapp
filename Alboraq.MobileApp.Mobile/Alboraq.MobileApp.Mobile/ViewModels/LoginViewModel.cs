using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Views;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService _accountService;
        //private readonly ICacheService _cacheService;
        private readonly INavigationService _navigationService;
       

        public LoginViewModel(IAccountService accountService, INavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;
            
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value;
                OnPropertyChanged("Username");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand SignInCommand
        {
            //TODO: REPLACE
            get
            {
                return new Command(async ()=> 
                {
                    var isSuccess = await _accountService.LoginAsync(Username, Password);
                                        
                    if (isSuccess)
                    {
                        App.Current.MainPage = new TabbedPage()
                        {
                            Children =
                            {
                                new HomePage() { Title = "Home"},

                            }
                        };
                    }
                    else
                    {                        
                        await _navigationService.DisplayAlert("Failed", "Login failed! Please try again.", "Ok", "Cancel");
                    }
                });
            }
        }
    }
}
