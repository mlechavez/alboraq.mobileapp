using System.ComponentModel.DataAnnotations;

namespace Alboraq.MobileApp.WebApi.Models.Admin.Users
{
    public class AddRoleBindingModel
    {
        [Required]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }

    public class UpdateRoleBindingModel
    {
        [Required]
        public string RoleID { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}