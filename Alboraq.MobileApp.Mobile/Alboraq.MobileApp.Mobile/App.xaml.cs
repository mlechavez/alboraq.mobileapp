using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using Alboraq.MobileApp.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile
{
    public partial class App : Application
    {
        public static WelcomeViewModel WelcomeViewModel { get; set; }
        public static LoginViewModel LoginViewModel { get; set; }
        public static RegisterViewModel RegisterViewModel { get; set; }

        public static Page LoginPage { get; private set; }
        public static Page RegisterPage { get; private set; }

        public App()
        {
            InitializeComponent();
           
            //abstract navigation 
            var navigationService = new NavigationService();

            WelcomeViewModel = new WelcomeViewModel(navigationService);
            LoginViewModel = new LoginViewModel(null, navigationService);
            RegisterViewModel = new RegisterViewModel(null, navigationService);

            MainPage = new NavigationPage(new WelcomePage());
            LoginPage = new LoginPage();
            RegisterPage = new RegisterPage();
            
            navigationService.Navigation = MainPage.Navigation;
            navigationService.MyPage = MainPage;
            
        }

        void SetViewModels()
        {

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
