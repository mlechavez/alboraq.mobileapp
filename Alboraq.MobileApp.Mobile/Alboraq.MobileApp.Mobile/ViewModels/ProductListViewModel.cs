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

namespace Alboraq.MobileApp.Mobile.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        public ProductListViewModel()
        {

        }
        public INavigation Navigation { get; set; }
        public Page Page { get; set; }
        public IProductService ProductService { get; set; }

        private ObservableCollection<ProductModel> _list;

        public ObservableCollection<ProductModel> Products
        {
            get { return _list ?? (_list = new ObservableCollection<ProductModel>()); }
            set {                
                _list = value;
                OnPropertyChanged("ProductList");
            }
        }

        private string _selectedCategory;

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
