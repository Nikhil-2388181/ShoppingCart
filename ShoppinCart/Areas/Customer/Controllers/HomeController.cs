using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Repository.IRepository;
using Shop.Models;

namespace ShoppinCart.Areas.Customer.Controllers
{
    [Area("Customer")]
     public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofWork.Product.GetAll(includeProperties: "Category");
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            ShopCart cart = new()
            {
                Product = _unitofWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShopCart shopCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shopCart.ApplicationUserId=userId;

            ShopCart cartFromDb = _unitofWork.ShopCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shopCart.ProductId);

            if(cartFromDb != null)
            {
                //ShoppingCart already exists
                cartFromDb.Count += shopCart.Count;
                _unitofWork.ShopCart.Update(cartFromDb);
            }
            else
            {
                //add new record
                _unitofWork.ShopCart.Add(shopCart);
               
            }
            TempData["success"] = "Cart updated Successfully";
               
            _unitofWork.Save();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

