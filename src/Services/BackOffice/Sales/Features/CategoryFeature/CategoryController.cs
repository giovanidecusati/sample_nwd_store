using AutoMapper;
using BackOffice.Sales.Data.Contexts;
using BackOffice.Sales.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackOffice.Sales.Features.CategoryFeature
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly SalesDbContext _context;

        public CategoryController(SalesDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var category = Mapper.Map<Category>(model);
            var result = await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { category.Id }, category);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (category == null)
                return NotFound();

            category = Mapper.Map<CategoryViewModel, Category>(model);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);

            if (category == null)
                return NotFound();

            _context.Remove(category);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (category == null)
                return NotFound();

            return Ok(Mapper.Map<CategoryViewModel>(category));
        }
    }
}