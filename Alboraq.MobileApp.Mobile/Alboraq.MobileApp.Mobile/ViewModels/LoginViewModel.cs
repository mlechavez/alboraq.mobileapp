using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Views;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
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

        private bool _canLogin = true;

        public bool CanLogin 
        {
            get { return _canLogin; }
            set {
                if(_canLogin != value)
                {
                    _canLogin = value;
                    OnPropertyChanged("CanLogin");
                    ((Command)SignInCommand).ChangeCanExecute();
                }                
            }
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

        private string _btnMsg;

        public string BtnMessage
        {
            get { return _btnMsg ?? (_btnMsg = "Sign in"); }
            set { _btnMsg = value;
                OnPropertyChanged("BtnMessage");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SignInCommand
        {            
            get
            {
                return new Command(async ()=> 
                {
                    CanLogin = false;
                    BtnMessage = "Signing in... please wait";
                    var response = await _accountService.LoginAsync(Username, Password);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        CanLogin = true;
                        BtnMessage = "Sign in";
                        App.Current.MainPage = new TabbedPage()
                        {
                            Children =
                            {
                                new HomePage() { Title = "Home"},
                                new AboutPage() { Title = "About"}
                            }
                        };
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        ErrorLogin errorLogin = JsonConvert.DeserializeObject<ErrorLogin>(content);                                                

                        await _navigationService.DisplayAlert("Login failed", $"{errorLogin.Description}", "Ok", "Cancel");
                    }
                }, ()=> CanLogin);
            }
        }
    }
}
