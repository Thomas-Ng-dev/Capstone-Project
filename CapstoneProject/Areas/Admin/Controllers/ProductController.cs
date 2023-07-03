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
        // for file uploads, access to wwwroot folder
        private readonly IWebHostEnvironment _webHostEnvironment; 
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Customer").ToList();
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
        // Combine update and insert operations into a single page, "Upsert"
        public IActionResult Upsert(int? id)
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
            if(id == null || id == 0)
            {
                // Create/insert view
                return View(productVM);
            }
            else
            {
                // Edit/Update view
                productVM.Product = _unitOfWork.Product.Get(product => product.Id == id);
                return View(productVM);
            }
           
        }
        [HttpPost] 
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            // Custom validations
            // TODO, Validation no longer working, does not stop loading and validation
            // messages no longer present, does not happen everytime? Why?


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
                // folder path for images
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    // generates a unique name for the file
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    // directory to save new image
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    // delete old image if you want to replace it in update
                    if (!string.IsNullOrEmpty(productVM.Product.ImageURL))
                    {
                        
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.
                            TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    // ImageURL starts as empty or null, set it with the new saved image
                    productVM.Product.ImageURL = @"\images\product\" + fileName;
                }

                // Insert SQL query
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                // Update SQL query
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                
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
        Temporary until combined view is tested
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
            Product? product = _unitOfWork.Product.Get(product => product.Id == id);
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

        #region API Calls
        // EF core method to make an api that returns all data as a JSON through the getall endpoint
        // Use this for DataTables library
        //[HttpGet]
        //public IActionResult GetAll(int id) 
        //{
        //    List<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Customer").ToList();
        //    return Json(new { data = productList});
        //}

        //public IActionResult Delete(int id)
        //{
        //    var productDeletion = _unitOfWork.Product.Get(prod => prod.Id == id);
        //    if (productDeletion == null)
        //    {
        //        return Json(new { success = false, message = "Error" });
        //    }
        //    var prodImgDeletion = Path.Combine(_webHostEnvironment.WebRootPath, productDeletion.ImageURL.TrimStart('\\'));
        //    if (System.IO.File.Exists(prodImgDeletion))
        //    {
        //        System.IO.File.Delete(prodImgDeletion);
        //    }
        //    _unitOfWork.Product.Remove(productDeletion);
        //    _unitOfWork.Save();

        //    return Json(new { success = true, message = "Deletion successful" });
        //}

        #endregion
    }
}
