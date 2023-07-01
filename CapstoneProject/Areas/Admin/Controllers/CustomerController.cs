using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        // Access ApplicationDbContext which is the sql database
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Store list of all customers from table
            List<Customer> customerList = _unitOfWork.Customer.GetAll().ToList();
            return View(customerList);
        }
        // Action to go to new path/page, goes to Customer/Create URL path
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] // The page "Create" uses a post method to pass information
        public IActionResult Create(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                // This executes the insert SQL statement
                _unitOfWork.Customer.Add(newCustomer);
                // Changes to database only applied after SaveChanges
                _unitOfWork.Save();
                // TempData is a key-value pair that is useable on the next page render, only once
                TempData["success"] = "Customer created.";
                // Redirect back to Customer/Index, can also be used to redirect to another controller
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
            Customer? customer = _unitOfWork.Customer.Get(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Customer.Update(customer);
                _unitOfWork.Save();
                TempData["success"] = "Customer updated.";
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
            Customer? customer = _unitOfWork.Customer.Get(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            _unitOfWork.Customer.Remove(customer);
            _unitOfWork.Save();
            TempData["success"] = "Customer deleted.";
            return RedirectToAction("Index");
        }
    }
}
