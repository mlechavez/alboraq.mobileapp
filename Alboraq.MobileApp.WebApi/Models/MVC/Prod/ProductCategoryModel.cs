using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Alboraq.MobileApp.WebApi.Models.MVC.Prod
{
    public class ProductCategoryModel
    {
        [Required]
        [Display(Name ="Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }
        [Required]
        [Display(Name = "Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}