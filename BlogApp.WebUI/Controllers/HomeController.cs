using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public HomeController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            var pageModel = new HomeBlogModel
            {
                HomeBlogs = _blogRepository.GetAll().Where(x => x.IsApproved && x.IsHome).ToList(),
                SliderBlogs = _blogRepository.GetAll().Where(x => x.IsApproved && x.IsSlider).ToList()
            };

            return View(pageModel);
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}