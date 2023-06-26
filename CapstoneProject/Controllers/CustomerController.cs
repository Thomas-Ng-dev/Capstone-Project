using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
