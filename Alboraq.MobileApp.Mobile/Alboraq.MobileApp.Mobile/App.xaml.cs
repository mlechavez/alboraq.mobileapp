using Alboraq.MobileApp.Mobile.Views;
using Xamarin.Forms;
using Akavache;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Reactive.Linq;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Renderers;

namespace Alboraq.MobileApp.Mobile
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();

            SetWelcomePage();
        }        

        private void SetWelcomePage()
        {
            var welcomePage = new WelcomePage();
            NavigationPage.SetHasNavigationBar(welcomePage, false);
            NavigationPage.SetBackButtonTitle(welcomePage, "Welcome");
            Current.MainPage = new PorscheNavigationPage(welcomePage);
        }

        public static void SetHomePage()
        {
            var tabbedPage = new TabbedPage()
            {
                BarBackgroundColor = Color.Black,
                Children =
                {
                    new HomePage(),
                    new MessagePage(),
                    new CallUsPage(),
                    new DirectionsPage(),
                    new AboutPage(),
                }
            };
            NavigationPage.SetHasNavigationBar(tabbedPage, false);
            App.Current.MainPage = new PorscheNavigationPage(tabbedPage);
        }

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
                App.SetHomePage();
            }
        }

        protected override void OnSleep()
        {
            //BlobCache.Shutdown().Wait();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
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
                App.SetHomePage();
            }
        }
    }
}
