using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Data.Data;
using Shop.Data.Repository;
using Shop.Data.Repository.IRepository;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Utility;

namespace ShoppinCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        { 
            List<Product> objProductList = _unitofWork.Product.GetAll(includeProperties:"Category").ToList();
            
            return View(objProductList);
        }
       
        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                CategoryList = _unitofWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
                //create
            }
            else
            {
                productVM.Product = _unitofWork.Product.Get(u => u.Id == id);
                return View(productVM);
                //update
            }


        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                //if (productVM.Product.Id == 0)
                //{
                //    _unitOfWork.Product.Add(productVM.Product);
                //}
                //else
                //{
                //    _unitOfWork.Product.Update(productVM.Product);
                //}

                //_unitOfWork.Save();
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {  //deleting the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\Images\product\" + fileName;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitofWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitofWork.Product.Update(productVM.Product);
                }
                _unitofWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitofWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitofWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _unitofWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitofWork.Product.Remove(obj);
            _unitofWork.Save();
            TempData["success"] = "CATEGORY DELETED SUCCESSFULLY";

            return RedirectToAction("Index");
        }
    }
}
