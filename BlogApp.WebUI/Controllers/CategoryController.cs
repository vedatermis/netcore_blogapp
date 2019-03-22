using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            return View(_categoryRepository.GetAll());
        }

        /* Kullanılmıyor
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(category);
                return RedirectToAction("List");
            }
            return View(category);
        }

    */

        public IActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return View(new Category());
            }
            else
            {
                return View(_categoryRepository.GetById((int) id));
            }
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.SaveCategory(category);
                TempData["Message"] = $"{category.Name} kayıt edildi.";

                return RedirectToAction("List");
            }
            else
            {
                return View(category);
            }
        }
    }
}