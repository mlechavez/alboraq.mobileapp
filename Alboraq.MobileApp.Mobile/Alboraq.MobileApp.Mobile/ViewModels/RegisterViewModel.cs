using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Views;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IAccountService _accountService;

        public RegisterViewModel(IAccountService accountService, INavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;
        }

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

        public bool CanRegister
        {
            get { return _canRegister; }
            set
            {
                _canRegister = value;
                OnPropertyChanged("CanRegister");
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var response = await _accountService.RegisterAsync(RegisterModel);

                    if (response.IsSuccessStatusCode)
                    {
                        App.Current.MainPage = new TabbedPage()
                        {
                            Children =
                            {
                                new HomePage() { Title = "Home"},
                                new AboutPage() { Title = "About"},
                            }
                        };
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

                        await _navigationService.DisplayAlert("Registration failed", $"{strBuilder.ToString()}", "Ok", "Cancel");

                    }
                }, () => CanRegister);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
