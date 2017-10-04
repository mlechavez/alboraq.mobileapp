using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Renderers;
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
       

        public LoginViewModel()
        {            
                        
        }
        public INavigation Navigation { get; set; }
        public IAccountService AccountService { get; set; }
        public Page Page { get; set; }

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
                return new Command( ()=> 
                {
                    App.SetHomePage();

                    //CanLogin = false;
                    //BtnMessage = "Signing in... please wait";
                    //var response = await _accountService.LoginAsync(Username, Password);
                    
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    CanLogin = true;
                    //    BtnMessage = "Sign in";
                    //    App.SetMainPage();                    
                    //}
                    //else
                    //{
                    //    var content = await response.Content.ReadAsStringAsync();

                    //    ErrorLogin errorLogin = JsonConvert.DeserializeObject<ErrorLogin>(content);                                                

                    //    await _navigationService.DisplayAlert("Login failed", $"{errorLogin.Description}", "Ok", "Cancel");
                    //}
                }, ()=> CanLogin);
            }
        }

        
    }
}
