using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
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
                return new Command(()=> {
                    _navigationService.DisplayAlert("Title", $"{RegisterModel.Email} {RegisterModel.Password} {RegisterModel.ConfirmPassword} {RegisterModel.PlateNo} {RegisterModel.MobileNo}", "Accept daw", "Cancel daw");
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
