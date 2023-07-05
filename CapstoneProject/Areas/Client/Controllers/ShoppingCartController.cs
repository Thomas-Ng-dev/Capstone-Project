using Microsoft.AspNetCore.Mvc;

namespace CapstoneProject.Areas.Client.Controllers
{
    [Area("Client")]
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
