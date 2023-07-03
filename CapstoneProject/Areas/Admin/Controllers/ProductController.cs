using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            // EF Projections
            // We can only pass one list to the next view, therefore we do not have access to Customer List
            // This projection stores the customer list into a new collection
            // This is a ViewBag, passes data from controller to a view.
            IEnumerable<SelectListItem> CustomerList = _unitOfWork.Customer.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            return View(productList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CustomerList = _unitOfWork.Customer.GetAll().Select(
                customer => new SelectListItem
                {
                    Text = customer.Name,
                    Value = customer.Id.ToString()
                });
            // ViewBag.CustomerList must be typed exactly, intellisense will not detect this
            // because it is not dynamic
            //ViewBag.CustomerList = CustomerList;
            //ViewData["CustomerList"] = CustomerList;
            ProductVM productVM = new()
            {
                CustomerList = CustomerList,
                Product = new Product()
            };
            return View(productVM);
        }
        [HttpPost] 
        public IActionResult Create(ProductVM productVM)
        {
            // Custom validations
            // TODO, Validation no longer working, does not stop loading and validation
            //  messages no longer present


            if (productVM.Product.Price <= productVM.Product.BulkRate10)
            {
                ModelState.AddModelError("BulkRate10", "Bulk price must be lower than the base price.");
            }
            if (productVM.Product.BulkRate10 <= productVM.Product.BulkRate100)
            {
                ModelState.AddModelError("BulkRate100", "Bulk price for this quantity is too high.");
            }
            if(productVM.Product.GrossWeight <= productVM.Product.NetWeight)
            {
                ModelState.AddModelError("GrossWeight", "Gross weight must be larger than net weight.");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created.";
                return RedirectToAction("Index");
            }
            else
            {
                // If you fail to create, the drop down will not be repopulated for customers.
                // Just reassign a recreated the drop down and pass it back to the view
                // when the page refreshes.
                productVM.CustomerList = _unitOfWork.Customer.GetAll().Select(
                customer => new SelectListItem
                {
                    Text = customer.Name,
                    Value = customer.Id.ToString()
                });
                return View(productVM);
            }
            
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
            // Seems to only be a problem for custom validation?
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
