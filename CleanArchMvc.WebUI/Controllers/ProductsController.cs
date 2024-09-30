using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        public readonly IProductService _productService;

        public readonly ICategoryService _categoryService;

        public readonly IWebHostEnvironment _environment;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
            new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {

                await _productService.Add(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetById(id);
            if (productDTO is null) return NotFound();

            var categories = await _categoryService.GetCategories();

            ViewBag.CategoryId =
            new SelectList(categories, "Id", "Name", productDTO.CategoryId);

            return View(productDTO);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetById(id);

            if (productDTO is null) return NotFound();

            return View(productDTO);
        }

        [HttpPost("delete"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);

            return RedirectToAction("Index");
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var productDTO = await _productService.GetById(id);

            if (productDTO is null) return NotFound();

            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, $"images\\{productDTO.Image}");
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(productDTO);
        }
    }
}