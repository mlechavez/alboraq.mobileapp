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
            vm = new ProductCategoryViewModel();
            vm.Navigation = Navigation;
            vm.ProductService = new ProductService();
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(async () => 
            {
                List<ProductCategoryModel> productCategories = await BlobCache.InMemory.GetObject<List<ProductCategoryModel>>("productCategories");

                if (productCategories == null)
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
            });
        }
    }
}