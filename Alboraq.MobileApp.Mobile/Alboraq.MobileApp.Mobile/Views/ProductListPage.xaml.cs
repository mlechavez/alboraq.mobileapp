using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alboraq.MobileApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        ProductListViewModel vm;
        public ProductListPage(string category)
        {
            InitializeComponent();
            vm = new ProductListViewModel
            {
                Navigation = Navigation,
                Page = this,
                SelectedCategory = category,
                ProductService = new ProductService()
            };
            BindingContext = vm;
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (vm.Products.Count == 0)
            {
                List<ProductModel> productList = new List<ProductModel>();

                try
                {
                    productList = await BlobCache.LocalMachine.GetObject<List<ProductModel>>("productList");

                    if (productList.Count > 0)
                    {
                        foreach (var product in productList)
                        {
                            vm.Products.Add(product);
                        }
                    }
                }
                catch (Exception)
                {
                    productList = await vm.ProductService.GetProductsByCategoryNameAsync(vm.SelectedCategory);

                    if (productList.Count > 0)
                    {
                        foreach (var product in productList)
                        {
                            vm.Products.Add(product);
                        }
                    }
                    await BlobCache.LocalMachine.InsertObject("productList", productList);
                }
                
            }
        }
    }    
}