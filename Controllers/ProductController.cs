using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAssignment.Data;
using NetCoreAssignment.Models;
using NetCoreAssignment.Models.Entities;

namespace NetCoreAssignment.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Cost = viewModel.Cost
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var product = await dbContext.Products.ToListAsync();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product viewModel)
        {
            var product = await dbContext.Products.FindAsync(viewModel.Id);
            if (product is not null)
            {
                product.Name = viewModel.Name;
                product.Cost = viewModel.Cost;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Product");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product viewModel)
        {
            var product = await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (product is not null)
            {
                dbContext.Products.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Product");
        }
    }
}
