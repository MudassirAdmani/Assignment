using ImgCrud.Data;
using Microsoft.AspNetCore.Mvc;

namespace ImgCrud.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var product = context.Products.ToList();
            return View(product);
        }
    }
}
