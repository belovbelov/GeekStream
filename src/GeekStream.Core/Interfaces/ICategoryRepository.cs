using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll();
    }
}