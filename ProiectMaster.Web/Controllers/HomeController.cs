using Microsoft.AspNetCore.Mvc;
using ProiectMaster.Models.Interfaces;
using System.Collections.Generic;

namespace ProiectMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var product = productService.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [Route("Add/{id}")]
        public IActionResult Add(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                shopList = new List<int>();

            if (!shopList.Contains(id))
                shopList.Add(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }

        [HttpPost]
        [Route("AddWishlist/{id}")]
        public IActionResult Wishlist(int id)
        {
            var wish = HttpContext.Session.Get<List<int>>(SessionHelper.Wish);

            if (wish == null)
                wish = new List<int>();

            if (!wish.Contains(id))
                wish.Add(id);

            HttpContext.Session.Set(SessionHelper.Wish, wish);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }

        [HttpPost]
        [Route("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                return RedirectToAction("Index", "Home", productService.GetAllProducts());

            if (shopList.Contains(id))
                shopList.Remove(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }

        [HttpPost]
        [Route("RemoveWishlist/{id}")]
        public IActionResult RemoveWishlist(int id)
        {
            var wish = HttpContext.Session.Get<List<int>>(SessionHelper.Wish);

            if (wish == null)
                return RedirectToAction("Index", "Home", productService.GetAllProducts());

            if (wish.Contains(id))
                wish.Remove(id);

            HttpContext.Session.Set(SessionHelper.Wish, wish);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }


    }
}
