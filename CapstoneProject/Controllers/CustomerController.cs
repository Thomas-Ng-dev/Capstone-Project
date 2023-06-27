using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    public class CustomerController : Controller
    {
        // Access ApplicationDbContext which is the sql database
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // Store list of all customers from table
            List<Customer> customerList = _db.Customers.ToList();
            return View(customerList);
        }
    }
}
