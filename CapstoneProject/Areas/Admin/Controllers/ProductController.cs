using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll().ToList();
            return View(productList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Create(Product newProduct)
        {
            // Custom validations
            if(newProduct.Price <= newProduct.BulkRate10)
            {
                ModelState.AddModelError("BulkRate10", "Bulk price must be lower than the base price.");
            }
            if (newProduct.BulkRate10 <= newProduct.BulkRate100)
            {
                ModelState.AddModelError("BulkRate100", "Bulk price for this quantity is too high.");
            }
            if(newProduct.GrossWeight <= newProduct.NetWeight)
            {
                ModelState.AddModelError("GrossWeight", "Gross weight must be larger than net weight.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(newProduct);
                _unitOfWork.Save();
                TempData["success"] = "Product created.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Returns object or null
            Product? product = _unitOfWork.Product.Get(x => x.Id == id);
            // Custom validations
            // TODO: This only evaluates the current values when you load the page, 
            // not the new ones being inputted in the fields
            if (product.Price <= product.BulkRate10)
            {
                ModelState.AddModelError("BulkRate10", "Bulk price must be lower than the base price.");
            }
            if (product.BulkRate10 <= product.BulkRate100)
            {
                ModelState.AddModelError("BulkRate100", "Bulk price for this quantity is too high.");
            }
            if (product.GrossWeight <= product.NetWeight)
            {
                ModelState.AddModelError("GrossWeight", "Gross weight must be larger than net weight.");
            }
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Returns object or null
            Product? product = _unitOfWork.Product.Get(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted.";
            return RedirectToAction("Index");
        }
    }
}
