using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using Alboraq.MobileApp.Mobile.Views;
using Xamarin.Forms;
using Akavache;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Alboraq.MobileApp.Mobile.Models;

namespace Alboraq.MobileApp.Mobile
{
    public partial class App : Application
    {                    
        public App()
        {
            
            InitializeComponent();
            
            //abstract navigation             
            var cacheService = new CacheService();
            var accountService = new AccountService();

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

        public static WelcomeViewModel WelcomeViewModel { get; set; }
        public static LoginViewModel LoginViewModel { get; set; }
        public static RegisterViewModel RegisterViewModel { get; set; }
        public static HomeViewModel HomeViewModel { get; set; }

        public static Page LoginPage { get; private set; }
        public static Page RegisterPage { get; private set; }     
        
        public AppCredentials Test { get; set; }

        protected override void OnStart()
        {
            base.OnStart();

            BlobCache.ApplicationName = "AlboraqApp";
            BlobCache.EnsureInitialized();

            try
            {                
                BlobCache.UserAccount.GetObject<AppCredentials>("login")
                .Subscribe(x => Test = x, ex => Debug.WriteLine("No Key"));
            }
            catch (KeyNotFoundException)
            {
                
            }            
            if (Test != null)
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
        }

        protected override void OnSleep()
        {            
            BlobCache.Shutdown().Wait();            
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
