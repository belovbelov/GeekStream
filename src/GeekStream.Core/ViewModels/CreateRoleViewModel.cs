using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}