using Alboraq.MobileApp.Core;
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
        public ActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        [Route("new-product")]
        public ActionResult NewProduct(ProductModel prod)
        {
            if (!ModelState.IsValid)
            {
                return View(prod);
            }
            var newProduct = new Product
            {
                ProductNo = prod.ProductNo.ToUpper(),
                ProductName = prod.ProductName.ToUpper(),
                ProductDescription = prod.ProductDescription.ToUpper(),
                UnitPrice = prod.UnitPrice,
                Qoh = prod.Qoh                
            };
            HttpPostedFileBase file = Request.Files["Image"];

            newProduct.Image = ConvertToBytes(file);

            _unitOfWork.Products.Add(newProduct);
            _unitOfWork.SaveChanges();

            return RedirectToAction("ProductList");
        }

        //TODO: MODIFY THIS
        [Route("retrieveimage")]
        public ActionResult RetrieveImage(int ID)
        {
            Product product = null;
            Task.Run(async () => {
                product = await _unitOfWork.Products.GetAsync(ID);
                Debug.WriteLine(product.Image);
                return File(product.Image, "image/jpg");
            });
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
    }
}