using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using Alboraq.MobileApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using Alboraq.MobileApp.Mobile.Helpers;
using System.Diagnostics;
using System.Collections;

namespace Alboraq.MobileApp.Mobile
{
    public partial class App : Application
    {
        public static WelcomeViewModel WelcomeViewModel { get; set; }
        public static LoginViewModel LoginViewModel { get; set; }
        public static RegisterViewModel RegisterViewModel { get; set; }
        public static HomeViewModel HomeViewModel { get; set; }

        public static Page LoginPage { get; private set; }
        public static Page RegisterPage { get; private set; }

             

        public App()
        {
            InitializeComponent();

            //abstract navigation             
            var cacheService = new CacheService();
            var accountService = new AccountService(cacheService);


            Task.Run(async () => 
            {
                IEnumerable<string> keys = await cacheService.GetAllKeys();
                foreach (var item in keys)
                {
                    Debug.WriteLine(item);
                }
                
            });

            bool loggedin = false;

            if (loggedin)
            {
                MainPage = new TabbedPage()
                {
                    Children =
                    {
                        new HomePage() { Title = "Title" }
                    }
                };
            }
            else
            {
                var navigationService = new NavigationService();
                WelcomeViewModel = new WelcomeViewModel(navigationService);
                LoginViewModel = new LoginViewModel(accountService, navigationService);
                RegisterViewModel = new RegisterViewModel(accountService, navigationService);



                MainPage = new NavigationPage(new WelcomePage());
                LoginPage = new LoginPage();
                RegisterPage = new RegisterPage();
                navigationService.Navigation = MainPage.Navigation;
                navigationService.MyPage = MainPage;
            }                                    
        }

        async Task GetCredentials(IAccountService accountService)
        {            
            await accountService.IsLoggedIn();                        
        }
        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
