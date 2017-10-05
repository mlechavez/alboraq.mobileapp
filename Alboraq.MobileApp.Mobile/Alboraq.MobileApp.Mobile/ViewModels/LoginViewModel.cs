using Akavache;
using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {        
       

        public LoginViewModel()
        {
            BlobCache.ApplicationName = "AlboraqApp";
            BlobCache.EnsureInitialized();
            SignInCommand = new Command(async () => await SimulateLoginAsync(), () => _canSignIn);
        }

        public INavigation Navigation { get; set; }
        public IAccountService AccountService { get; set; }
        public Page Page { get; set; }

        private bool _canSignIn = true;
        
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

        public ICommand SignInCommand { get; private set; }     

        async Task SimulateLoginAsync()
        {
            CanInitiateSignIn(false);
            BtnMessage = "Signing in.. please wait.";
            var response = await AccountService.LoginAsync(Username, Password);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                AppCredentialsModel login = JsonConvert.DeserializeObject<AppCredentialsModel>(responseContent);
                await BlobCache.Secure.InsertObject("login", login);

                CanInitiateSignIn(true);
                BtnMessage = "Sign in";
                App.SetHomePage();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                ErrorLogin errorLogin = JsonConvert.DeserializeObject<ErrorLogin>(content);
                await Page.DisplayAlert("Login failed", $"{errorLogin.Description}", "Ok");
                CanInitiateSignIn(true);
                BtnMessage = "Sign in";
            }
        }

        private void CanInitiateSignIn(bool v)
        {
            _canSignIn = v;
            ((Command)SignInCommand).ChangeCanExecute();            
        }        
    }
}
