using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels
{
    public class LoginViewModel
    {
            [Required]
            [Display(Name = "Никнейм")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Display(Name = "Запомнить этот компьютер?")]
            public bool RememberMe { get; set; }
    }
}