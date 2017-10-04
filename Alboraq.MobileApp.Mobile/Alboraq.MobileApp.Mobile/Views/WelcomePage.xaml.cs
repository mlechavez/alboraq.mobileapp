using Alboraq.MobileApp.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alboraq.MobileApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {        
        public WelcomePage()
        {
            InitializeComponent();
            
            var vm = new WelcomeViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;              
        }       
    }
}