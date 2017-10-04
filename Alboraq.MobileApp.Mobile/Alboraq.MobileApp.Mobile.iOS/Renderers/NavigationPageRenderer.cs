using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Alboraq.MobileApp.Mobile.Renderers;
using Alboraq.MobileApp.Mobile.iOS.Renderers;

[assembly: ExportRenderer(typeof(PorscheNavigationPage), typeof(PorscheNavigationPageRenderer))]
namespace Alboraq.MobileApp.Mobile.iOS.Renderers
{
    public class PorscheNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var att = new UITextAttributes();
                att.Font = UIFont.FromName("PorscheNextTT-Thin", 20);
                UINavigationBar.Appearance.SetTitleTextAttributes(att);
            }
        }
    }
}