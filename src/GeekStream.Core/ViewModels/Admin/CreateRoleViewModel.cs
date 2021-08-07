using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Admin
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}