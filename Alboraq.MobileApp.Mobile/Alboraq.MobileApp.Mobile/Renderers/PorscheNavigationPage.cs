using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Alboraq.MobileApp.Mobile.Renderers
{
    public class PorscheNavigationPage : NavigationPage
    {
        public PorscheNavigationPage(Page root)
            :base(root)
        {
            BarBackgroundColor = Color.Black;
            BarTextColor = Color.White;            
        }
    }
}
