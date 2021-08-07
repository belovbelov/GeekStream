using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels.Account
{
    public class LoginViewModel
    {
            [Display(Name = "Адрес электронной почты")]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Display(Name = "Запомнить этот компьютер?")]
            public bool RememberMe { get; set; }
    }
}