using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels
{
    public class ArticleEditViewModel 
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Придумайте название статьи")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст статьи")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Выберите категорию для статьи")]
        public int CategoryId{ get; set; }

        [Required]
        [Display(Name = "Укажите ключевые слова")]
        public string Keywords { get; set; }

        [Display(Name = "Выберите изображения")]
        public List<FilePath> FilePaths { get; set; } = new List<FilePath>();

        public ArticleType ArticleType { get; set; }
    }
}