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
using Alboraq.MobileApp.Mobile.Views;

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class ProductCategoryViewModel : INotifyPropertyChanged
    {
        public ProductCategoryViewModel()
        {
            
        }
        public INavigation Navigation { get; set; }
        public IProductService ProductService { get; set; }
        public Page Page { get; set; }

        private ObservableCollection<ProductCategoryModel> _productCategories;
        public ObservableCollection<ProductCategoryModel> ProductCategories
        {
            get { return _productCategories ?? (_productCategories = new ObservableCollection<ProductCategoryModel>()); }
            set {                
                _productCategories = value;
                OnPropertyChanged("ProductCategories");                
            }
        }

        private ProductCategoryModel _productCategory;

        public ProductCategoryModel ProductCategory
        {
            get { return _productCategory; }
            set {
                if(_productCategory != value)
                {
                    _productCategory = value;
                    OnPropertyChanged("ProductCategory");
                    GotoPage(_productCategory.CategoryName);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;        

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        void GotoPage(string category)
        {
            var viewModel = new ProductListViewModel
            {
                SelectedCategory = category,
                Navigation = Navigation
            };

            var productsPage = new ProductListPage(category) { Title = category };

            Navigation.PushAsync(productsPage, animated: true);
        }
    }
}
