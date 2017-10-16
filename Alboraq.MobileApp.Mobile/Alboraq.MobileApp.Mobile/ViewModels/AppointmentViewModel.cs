using Akavache;
using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reactive.Linq;
using Alboraq.MobileApp.Mobile.Views;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {        
        private DateTime _minDate;
        private bool canSetAppointment = true;        
        public AppointmentViewModel()
        {            
            SetAppointmentCommand = new Command(async () => await SimulateSetAppointment(), ()=> canSetAppointment);            
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
            get { return _accounInfo; }
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
        
        async Task SimulateSetAppointment()
        {                                  
            CanInitiateSetAppointment(false);

            var appointment = new AppointmentModel
            {
                CustomerName = AccountInfo.CustomerName,
                PlateNo = AccountInfo.PlateNo,
                MobileNo = AccountInfo.MobileNo,
                AppointmentDate = SelectedDate,
                Email = App.AppCredentials.Username
            };

            //TODO: CHANGE THE MINIMUM DATE....
            var response = await AppointmentService.SetAppointmentAsync(App.AppCredentials.Username, App.AppCredentials.AccessToken, appointment);

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
