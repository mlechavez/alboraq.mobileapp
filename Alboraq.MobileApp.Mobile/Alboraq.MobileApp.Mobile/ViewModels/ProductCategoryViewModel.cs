using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class ProductCategoryViewModel : INotifyPropertyChanged
    {
        public ProductCategoryViewModel()
        {
            
        }
        public INavigation Navigation { get; set; }
        public IProductService ProductService { get; set; }

        private ObservableCollection<ProductCategoryModel> _productCategories;
        public ObservableCollection<ProductCategoryModel> ProductCategories
        {
            get { return _productCategories ?? (_productCategories = new ObservableCollection<ProductCategoryModel>()); }
            set {                
                _productCategories = value;
                OnPropertyChanged("ProductCategories");                
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;        

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
    }
}
