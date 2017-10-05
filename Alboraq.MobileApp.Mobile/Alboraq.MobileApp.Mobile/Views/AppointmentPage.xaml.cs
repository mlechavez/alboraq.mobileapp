using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alboraq.MobileApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentPage : ContentPage
    {
        public AppointmentPage()
        {
            InitializeComponent();
            var vm = new AppointmentViewModel();
            vm.Navigation = Navigation;
            vm.Page = this;
            vm.AppointmentService = new AppointmentService();
            BindingContext = vm;            
        }
    }
}