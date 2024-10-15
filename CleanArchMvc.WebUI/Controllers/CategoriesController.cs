using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class CategoriesController : Controller
    {
        public ICategoryService _categoryService { get; set; }

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var categoryDTO = await _categoryService.GetById(id);

            if (categoryDTO is null) return NotFound();

            return View(categoryDTO);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(categoryDto);
                }
                catch (Exception)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(categoryDto);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var categoryDto = await _categoryService.GetById(id);

            if (categoryDto is null) return NotFound();

            return View(categoryDto);
        }

        // Teve que renomear pois tinha a mesma assinatura 
        //do outro Delete
        [HttpPost("delete"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            CategoryDTO categoryDto = await _categoryService.GetById(id);

            if (categoryDto is null) return NotFound();

            return View(categoryDto);
        }
    }
}