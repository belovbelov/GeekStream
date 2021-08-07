using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Admin
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        public string RoleName { get; set; }

        public List<string> Users { get; set; } = new();
    }
}