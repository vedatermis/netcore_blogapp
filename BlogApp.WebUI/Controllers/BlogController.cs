using System;
using System.Linq;
using System.Net;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index(int? id, string q)
        {
            var query = _blogRepository.GetAll().Where(x => x.IsApproved);

            if (id != null)
            {
                query = query.Where(i => i.CategoryId == id);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(i => EF.Functions.Like(i.Title, $"%{ q }%") || EF.Functions.Like(i.Description, $"%{ q }%") || EF.Functions.Like(i.Body, $"%{ q }%"));
            }

            return View(query.OrderByDescending(i => i.Date));
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

        /* Kullanılmıyor
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
        */
        
        public IActionResult AddOrUpdate(int? id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");

            if (id == null)
            {
                return View(new Blog());
            }

            return View(_blogRepository.GetById((int)id));
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.SaveBlog(blog);
                TempData["Message"] = $"{blog.Title} kayıt edildi.";
                return RedirectToAction("List");
            }

            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
            return View(blog);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_blogRepository.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogRepository.DeleteBlog(id);
            TempData["Message"] = $"{id} numaralı kayıt silindi.";
            return RedirectToAction("List");

        }

        public IActionResult Details(int id)
        {
            return View(_blogRepository.GetById(id));
        }
    }
}