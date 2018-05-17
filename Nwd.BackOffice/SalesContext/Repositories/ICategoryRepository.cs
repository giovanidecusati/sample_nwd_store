using Nwd.BackOffice.SalesContext.Entities;
using System;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category entity);

        void Update(Category entity);

        void Delete(Category entity);

        Task<Category> GetCategoryByIdAsync(Guid categoryId);
    }
}
