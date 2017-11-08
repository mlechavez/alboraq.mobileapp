using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Alboraq.MobileApp.WebApi.Areas.Admin.Models
{
    public class ProductModel
    {        
        [Required]
        [Display(Name ="Product no")]
        public string ProductNo { get; set; }
        [Required]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public decimal Qoh { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int? ProductCategoryID { get; set; }
        
        public byte[] Image { get; set; }
        [Required]
        [Display(Name = "Image")]
        public HttpPostedFileBase PostedImage { get; set; }
    }
}