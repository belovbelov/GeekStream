using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.ViewModels
{
    public class ArticleCreationViewModel 
    {
        [Required]
        [Display(Name = "Придумайте название статьи")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст статьи")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Выберите категорию для статьи")]
        public int CategoryId{ get; set; }
    }
}