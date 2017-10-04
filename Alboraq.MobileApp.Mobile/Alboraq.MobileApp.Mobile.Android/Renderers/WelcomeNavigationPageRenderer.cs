using System;
using Xamarin.Forms;
using Android.Graphics;
using Alboraq.MobileApp.Mobile.Renderers;
using Alboraq.MobileApp.Mobile.Droid.Helpers;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(PorscheNavigationPage), typeof(PorscheNavigationPageRenderer))]
namespace Alboraq.MobileApp.Mobile.Droid.Helpers
{
    public class PorscheNavigationPageRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (view == typeof(Android.Widget.TextView))
            {
                var textView = (Android.Widget.TextView)e.Child;
                var porscheFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "porsche-next-thin.ttf");
                textView.Typeface = porscheFont;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }            
        }
    }
}