using Alboraq.MobileApp.Mobile.Helpers;
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
    public partial class CallUsPage : ContentPage
    {        
        public CallUsPage()
        {            
            InitializeComponent();

            var vm = new CallUsViewModel();
            vm.Navigation = Navigation;
            vm.Page = this;         
            BindingContext = vm;
        }

        //private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //  var isProceed =  await DisplayAlert("Which location would you like to call?", "you hit me", "Ok", "cancel");

        //    if (isProceed)
        //    {
        //        Device.OpenUri(new Uri("tel:+97470064955"));
        //    }
        //}
    }
}