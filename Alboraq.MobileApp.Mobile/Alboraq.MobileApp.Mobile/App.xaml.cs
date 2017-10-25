using Alboraq.MobileApp.Mobile.Views;
using Xamarin.Forms;
using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Renderers;

namespace Alboraq.MobileApp.Mobile
{
    public partial class App : Application
    {
        public App()
        {            
            InitializeComponent();
            BlobCache.ApplicationName = "AlboraqApp";
            BlobCache.EnsureInitialized();            

            SetWelcomePage();
        }
        public static AppCredentialsModel AppCredentials { get; internal set; }
        public static AccountInfoModel AccountInfo { get; internal set; }

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
                    new AboutPage()
                }
            };
            NavigationPage.SetHasNavigationBar(tabbedPage, false);
            Current.MainPage = new PorscheNavigationPage(tabbedPage);
        }        
        
        protected override void OnStart()
        {
            base.OnStart();                                  
        }

        protected override void OnSleep()
        {            
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            //Task.Run(async () =>
            //{                
            //    AppCredentials = await BlobCache.Secure.GetObject<AppCredentialsModel>("login");

            //    if (AppCredentials != null)
            //    {
            //        SetHomePage();
            //    }
            //    else
            //    {
            //        SetWelcomePage();
            //    }


            //});  
            // Handle when your app resumes            
        }
    }
}
