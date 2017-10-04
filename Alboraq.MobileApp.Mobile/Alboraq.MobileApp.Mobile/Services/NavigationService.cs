using Alboraq.MobileApp.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation Navigation { get; internal set; }
        public Page NavigationPage { get; set; }

        public IReadOnlyList<Page> ModalStack
        {
            get
            {
                return Navigation.ModalStack;
            }
        }

        public IReadOnlyList<Page> NavigationStack
        {
            get
            {
                return Navigation.NavigationStack;
            }
        }

        public void InsertPageBefore(Page page, Page before)
        {
            Navigation.InsertPageBefore(page, before);
        }

        public Task<Page> PopAsync()
        {
            return Navigation.PopAsync();
        }

        public Task<Page> PopAsync(bool animated)
        {
            return Navigation.PopAsync(animated);
        }

        public Task<Page> PopModalAsync()
        {
            return Navigation.PopModalAsync();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            return Navigation.PopModalAsync(animated);
        }

        public Task PopToRootAsync()
        {
            return Navigation.PopToRootAsync();
        }

        public Task PopToRootAsync(bool animated)
        {
            return Navigation.PopToRootAsync(animated);
        }

        public Task PushAsync(Page page)
        {
            return Navigation.PushAsync(page);
        }

        public Task PushAsync(Page page, bool animated)
        {
            return Navigation.PushAsync(page, animated); 
        }

        public Task PushModalAsync(Page page)
        {
            return Navigation.PushModalAsync(page);
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            return Navigation.PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            Navigation.RemovePage(page);
        }

        public Task<bool> DisplayAlert(string title, string message, string accept, string cancel = null)
        {
            return NavigationPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
