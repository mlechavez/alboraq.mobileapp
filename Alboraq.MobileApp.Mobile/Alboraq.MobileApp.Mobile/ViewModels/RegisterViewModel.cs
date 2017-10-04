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
        
        public RegisterViewModel()
        {            
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

        private string _btnRegisterText;

        public string BtnRegisterText
        {
            get { return _btnRegisterText; }
            set { _btnRegisterText = value;
                OnPropertyChanged("BtnRegisterText");
            }
        }


        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    CanRegister = false;
                    BtnRegisterText = "Registering... please wait";
                    var response = await AccountService.RegisterAsync(RegisterModel);

                    if (response.IsSuccessStatusCode)
                    {
                        CanRegister = true;
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
