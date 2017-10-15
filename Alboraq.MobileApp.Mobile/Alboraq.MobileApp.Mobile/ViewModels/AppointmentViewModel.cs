using Akavache;
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
using System.Reactive.Linq;
using Alboraq.MobileApp.Mobile.Views;
using System.Diagnostics;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {        
        private DateTime _minDate;
        private bool canSetAppointment = true;        
        public AppointmentViewModel()
        {
            BlobCache.ApplicationName = "AlboraqApp";
            BlobCache.EnsureInitialized();
            SetAppointmentCommand = new Command(async () => await SimulateSetAppointment(), ()=> canSetAppointment);
            GetAccountInfo();
        }
        public INavigation Navigation { get; set; }
        public Page Page { get; set; }
        public IAppointmentService AppointmentService { get; set; }
        public IAccountService AccountService { get; set; }

        public DateTime MinimumDate
        {
            get { return _minDate = DateTime.Now.AddDays(1); }
            set { _minDate = value;
                OnPropertyChanged("MinimumDate");
            }
        }

        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate = DateTime.Now.AddDays(1); }
            set { _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private AccountInfoModel _accounInfo;

        public AccountInfoModel AccountInfo
        {
            get { return _accounInfo = new AccountInfoModel(); }
            set { _accounInfo = value;
                OnPropertyChanged("AccountInfo");
            }
        }
        
        public ICommand SetAppointmentCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void GetAccountInfo()
        {
            AppCredentialsModel appCredentialsModel = null;
                BlobCache.Secure.GetObject<AppCredentialsModel>("login")
                    .Subscribe(x => appCredentialsModel = x, () => Debug.WriteLine("No Key!"));

            Task.Run(async ()=> AccountInfo = await AccountService.GetAccountInfoAsync(appCredentialsModel.Username, appCredentialsModel.AccessToken));            
        }
        async Task SimulateSetAppointment()
        {
            AppCredentialsModel login = null;

            try
            {
                login = await BlobCache.Secure.GetObject<AppCredentialsModel>("login");
            }
            catch (Exception)
            {
                
            }

            if (login == null)
            {
                await Navigation.PopToRootAsync();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            
            CanInitiateSetAppointment(false);
            //TODO: CHANGE THE MINIMUM DATE....
            var response = await AppointmentService.SetAppointmentAsync(login.Username, login.AccessToken, MinimumDate);

            if (response.IsSuccessStatusCode)
            {
                await Page.DisplayAlert("Success!", "You will receive a message if your appointment has been confirmed.", "Ok");                
            }
            else
            {
                await Page.DisplayAlert("Failed!", "Something went wrong.", "Ok");
            }
            CanInitiateSetAppointment(true);
        }

        private void CanInitiateSetAppointment(bool v)
        {
            canSetAppointment = v;
            ((Command)SetAppointmentCommand).ChangeCanExecute();
        }
    }
}
