﻿using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.WebApi.Filters;
using Alboraq.MobileApp.WebApi.Models.MVC.Prod;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Alboraq.MobileApp.WebApi.Controllers.MVC
{
    [AccessActionFilter(RoleName = "parts salesman")]
    [RoutePrefix("products")]
    public class ProdController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
                
        public ActionResult Index()
        {            
            return View();
        }

        [Route("product-list")]
        public async Task<ActionResult> ProductList()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return View(products);
        }

        [Route("new-product")]
        public async Task<ActionResult> NewProduct()
        {
            var categories = await _unitOfWork.ProductCategories.GetAllAsync();
            ViewBag.ProductCategoryID = new SelectList(items: categories, dataValueField: "ProductCategoryID", dataTextField: "CategoryName");
            return View();
        }

        [HttpPost]
        [Route("new-product")]
        public ActionResult NewProduct(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }
            var newProduct = new Product
            {
                ProductNo = productModel.ProductNo.ToUpper(),
                ProductName = productModel.ProductName.ToUpper(),
                ProductDescription = productModel.ProductDescription.ToUpper(),
                UnitPrice = productModel.UnitPrice,
                Qoh = productModel.Qoh,
                ProductCategoryID = productModel.ProductCategoryID
            };
            HttpPostedFileBase file = Request.Files["Image"];

            newProduct.Image = ConvertToBytes(file);

            _unitOfWork.Products.Add(newProduct);
            _unitOfWork.SaveChanges();

            return RedirectToAction("ProductList");
        }

        
        [Route("RetrieveProductImage")]
        public async Task<ActionResult> RetrieveProductImage(string productNo)
        {
            
            var product = await _unitOfWork.Products.GetAsync(productNo);

            return File(product.Image, "image/jpg");            
        }

        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            using (BinaryReader reader = new BinaryReader(file.InputStream))
            {
                imageBytes = reader.ReadBytes((int)file.ContentLength);
            }
                        
            return imageBytes;
        }

        [Route("new-product-category")]
        public async Task<ActionResult> NewProductCategory()
        {
            var categories = await _unitOfWork.ProductCategories.GetAllAsync();
            var viewModel = new NewProductCategoryViewModel { ProductCategories = categories, ProductCategoryModel = new ProductCategoryModel() };
            return View(viewModel);
        }

        [HttpPost]
        [Route("new-product-category")]
        public ActionResult NewProductCategory(NewProductCategoryViewModel newProductCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(newProductCategoryViewModel.ProductCategoryModel);
            }

            var newCategory = new ProductCategory
            {
                CategoryName = newProductCategoryViewModel.ProductCategoryModel.CategoryName,
                CategoryDescription = newProductCategoryViewModel.ProductCategoryModel.CategoryDescription
            };
            newCategory.Image = ConvertToBytes(newProductCategoryViewModel.ProductCategoryModel.Image);

            _unitOfWork.ProductCategories.Add(newCategory);
            _unitOfWork.SaveChanges();

            return RedirectToAction("productlist");
        }

        [Route("RetrieveProductCategoryImage")]
        public async Task<ActionResult> RetrieveProductCategoryImage(int id)
        {
            var productCategory = await _unitOfWork.ProductCategories.GetAsync(id);

            return File(productCategory.Image, "image/jpg");
        }

        [Route("{productNo}/details")]
        public async Task<ActionResult> ProductDetails(string productNo)
        {
            var product = await _unitOfWork.Products.GetAsync(productNo);
            var categories = await _unitOfWork.ProductCategories.GetAllAsync();
            ViewBag.ProductCategoryID = new SelectList(items: categories, dataValueField: "ProductCategoryID", dataTextField: "CategoryName", selectedValue: product.ProductCategory.ProductCategoryID);
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProduct(Product productModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _unitOfWork.ProductCategories.GetAllAsync();
                ViewBag.ProductCategoryID = new SelectList(items: categories, dataValueField: "ProductCategoryID", dataTextField: "CategoryName", selectedValue: productModel.ProductCategoryID);
                return View(productModel);
            }
            var product = await _unitOfWork.Products.GetAsync(productModel.ProductNo);

            product.ProductName = productModel.ProductName;
            product.ProductDescription = productModel.ProductDescription;
            product.UnitPrice = productModel.UnitPrice;
            product.Qoh = productModel.Qoh;
            product.ProductCategoryID = productModel.ProductCategoryID;

            HttpPostedFileBase file = Request.Files["Image"];

            if (file != null)
            {
                product.Image = new byte[file.ContentLength];
                file.InputStream.Read(product.Image, 0, file.ContentLength);
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("productlist");
        }
    }
}