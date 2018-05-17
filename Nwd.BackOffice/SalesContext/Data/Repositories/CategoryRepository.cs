using Microsoft.EntityFrameworkCore;
using Nwd.BackOffice.SalesContext.Data.Contexts;
using Nwd.BackOffice.SalesContext.Entities;
using Nwd.BackOffice.SalesContext.Repositories;
using System;
using System.Threading.Tasks;

namespace Nwd.BackOffice.SalesContext.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SalesDbContext _context;

        public CategoryRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
        }

        public void Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(p => p.Id == categoryId);
        }

        public void Delete(Category entity)
        {
            _context.Categories.Remove(entity);
        }
    }
}
