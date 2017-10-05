using Akavache;
using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Views;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        
        public RegisterViewModel()
        {
            BlobCache.ApplicationName = "AlboraqApp";
            BlobCache.EnsureInitialized();

            RegisterCommand = new Command(async ()=> await SimulateRegisterAsync(), ()=> _canRegister);
        }

        public INavigation Navigation { get; set; }
        public IAccountService AccountService { get; set; }
        public Page Page { get; set; }

        private RegisterModel _registerModel;

        public RegisterModel RegisterModel
        {
            get { return _registerModel ?? (_registerModel = new RegisterModel()); }
            set
            {
                _registerModel = value;
                OnPropertyChanged("RegisterModel");
            }
        }

        private bool _canRegister = true;        

        private string _btnRegisterText;

        public string BtnRegisterText
        {
            get { return _btnRegisterText ?? (_btnRegisterText = "Register"); }
            set { _btnRegisterText = value;
                OnPropertyChanged("BtnRegisterText");
            }
        }

        public ICommand RegisterCommand { get; private set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        async Task SimulateRegisterAsync()
        {
            CanInitiateRegister(false);
            BtnRegisterText = "Registering... please wait.";
            var response = await AccountService.RegisterAsync(RegisterModel);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                AppCredentialsModel login = JsonConvert.DeserializeObject<AppCredentialsModel>(content);

                await BlobCache.Secure.InsertObject("login", login);

                CanInitiateRegister(true);
                BtnRegisterText = "Register";
                App.SetHomePage();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                ErrorRegister json = JsonConvert.DeserializeObject<ErrorRegister>(content);
                StringBuilder strBuilder = new StringBuilder();

                foreach (var item in json.ModelState.Values)
                {
                    foreach (var error in item)
                    {
                        strBuilder.AppendLine(error);
                    }
                }

                await Page.DisplayAlert("Registration failed", $"{strBuilder.ToString()}", "Ok", "Cancel");
                CanInitiateRegister(true);
                BtnRegisterText = "Register";
            }
        }

        void CanInitiateRegister(bool v)
        {
            _canRegister = v;
            ((Command)RegisterCommand).ChangeCanExecute();
        }
    }
}
