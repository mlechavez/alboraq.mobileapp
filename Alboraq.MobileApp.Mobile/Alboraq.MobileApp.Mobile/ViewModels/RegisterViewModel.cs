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
            set {
                _registerModel = value;
                OnPropertyChanged("RegisterModel");
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async ()=> {
                    var isSuccess = await _accountService.RegisterAsync(RegisterModel);
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
                        await _navigationService.DisplayAlert("Failed", "Registration failed!", "Ok", "Cancel");
                    }
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
