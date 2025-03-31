using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Repository.IRepository;
using Shop.Models;
using Shop.Models.ViewModels;
using System.Security.Claims;

namespace ShoppinCart.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public ShopCartVM ShopCartVM { get; set; }
        public CartController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShopCartVM = new()
            {
                ShopCartList = _unitofWork.ShopCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach(var cart in ShopCartVM.ShopCartList)
            {
                 cart.Price = GetPriceBasedOnQuantity(cart);
                ShopCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShopCartVM);
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShopCartVM = new()
            {
                ShopCartList = _unitofWork.ShopCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Product"),
                OrderHeader = new()
            };

            ShopCartVM.OrderHeader.ApplicationUser = _unitofWork.ApplicationUser.Get(u=>u.Id==userId);

            ShopCartVM.OrderHeader.Name = ShopCartVM.OrderHeader.ApplicationUser.Name;
            ShopCartVM.OrderHeader.PhoneNumber = ShopCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShopCartVM.OrderHeader.StreetAddress = ShopCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShopCartVM.OrderHeader.City = ShopCartVM.OrderHeader.ApplicationUser.City;
            ShopCartVM.OrderHeader.State = ShopCartVM.OrderHeader.ApplicationUser.State;
            ShopCartVM.OrderHeader.PostalCode = ShopCartVM.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cart in ShopCartVM.ShopCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                ShopCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShopCartVM);
        }

        public IActionResult plus(int cartId)
        {
            var cartFromDb = _unitofWork.ShopCart.Get(u=>u.Id == cartId);
            cartFromDb.Count += 1;
            _unitofWork.ShopCart.Update(cartFromDb);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int cartId)
        {
            var cartFromDb = _unitofWork.ShopCart.Get(u => u.Id == cartId);
            if(cartFromDb.Count <= 1)
            {
                //remove it from cart
                _unitofWork.ShopCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitofWork.ShopCart.Update(cartFromDb);
            }

               
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitofWork.ShopCart.Get(u => u.Id == cartId);
           
            _unitofWork.ShopCart.Remove(cartFromDb);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }





        private double GetPriceBasedOnQuantity(ShopCart shopCart)
        {
            if (shopCart.Count <= 50)
            {
                return shopCart.Product.Price;
            }
            else
            {
                if (shopCart.Count <= 100)
                {
                    return shopCart.Product.Price50;
                }
                else
                {
                    return shopCart.Product.Price100;
                }
            }
        }
    }
}
