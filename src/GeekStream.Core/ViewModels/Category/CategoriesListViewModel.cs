using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GeekStream.Core.ViewModels.Category
{
    public class CategoriesListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}