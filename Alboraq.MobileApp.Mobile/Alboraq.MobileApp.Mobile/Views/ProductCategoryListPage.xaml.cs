using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using Alboraq.MobileApp.Mobile.Services;
using Alboraq.MobileApp.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alboraq.MobileApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCategoryListPage : ContentPage
    {
        ProductCategoryViewModel vm;
        public ProductCategoryListPage()
        {
            InitializeComponent();
            vm = new ProductCategoryViewModel
            {
                Navigation = Navigation,
                ProductService = new ProductService()
            };
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            List<ProductCategoryModel> productCategories = new List<ProductCategoryModel>();

            try
            {
                productCategories = await BlobCache.InMemory.GetObject<List<ProductCategoryModel>>("productCategories");

                if (productCategories.Count > 0)
                {
                    foreach (var category in productCategories)
                    {
                        vm.ProductCategories.Add(category);
                    }
                }
            }
            catch (Exception)
            {
                if (productCategories.Count == 0)
                {
                    productCategories = await vm.ProductService.GetCategoriesAsync();

                    if (productCategories != null)

                    {
                        foreach (var category in productCategories)
                        {
                            vm.ProductCategories.Add(category);
                        }
                        await BlobCache.InMemory.InsertObject("productCategories", productCategories);
                    }
                }
            }                        
        }
    }
}