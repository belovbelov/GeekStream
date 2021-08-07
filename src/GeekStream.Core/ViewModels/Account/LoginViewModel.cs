using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Account
{
    public class LoginViewModel
    {
            [Required]
            [Display(Name = "Электронная почта")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Display(Name = "Запомнить этот компьютер?")]
            public bool RememberMe { get; set; }
    }
}