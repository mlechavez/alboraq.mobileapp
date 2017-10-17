using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alboraq.MobileApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentPage : ContentPage
    {
        AppointmentViewModel vm;

        public AppointmentPage()
        {
            InitializeComponent();
            vm = new AppointmentViewModel
            {
                Navigation = Navigation,
                Page = this,
                AppointmentService = new AppointmentService(),
                AccountService = new AccountService()
            };
            BindingContext = vm;            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.AccountInfo == null)
            {               
                Task.Run(async () =>
                {
                    App.AccountInfo = await BlobCache.Secure.GetObject<AccountInfoModel>("accountInfo");

                    if (App.AccountInfo == null)
                    {
                        AppCredentialsModel appCredentialsModel = null;
                        BlobCache.Secure.GetObject<AppCredentialsModel>("login")
                            .Subscribe(x => appCredentialsModel = x, () => Debug.WriteLine("No Key!"));

                        App.AccountInfo = await vm.AccountService.GetAccountInfoAsync(appCredentialsModel.Username, appCredentialsModel.AccessToken);
                        vm.AccountInfo = App.AccountInfo;
                    }
                    else
                    {
                        vm.AccountInfo = App.AccountInfo;
                    }
                });
            }
            else
            {
                vm.AccountInfo = App.AccountInfo;
            }            
        }
    }
}