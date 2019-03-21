using System;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            return View(_blogRepository.GetAll());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            blog.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                _blogRepository.AddBlog(blog);
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(blog);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(_blogRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.UpdateBlog(blog);

                TempData["Message"] = $"{blog.Title} güncellendi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(blog);
        }
    }
}