using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CapstoneProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    x => x.ApplicationUserId == userId,
                    includeProperties: "Product")
            };

            foreach(var item in ShoppingCartVM.ShoppingCartList)
            {
                item.ItemPrice = GetQuantityPrice(item);
                ShoppingCartVM.OrderTotal += (item.ItemPrice * item.Count);
            }
            return View(ShoppingCartVM);
        }
        // Check if standard or bulk pricing
        private double GetQuantityPrice(ShoppingCart cart)
        {
            if(cart.Count <= 10)
            {
                return cart.Product.Price;
            }
            else if(cart.Count <= 100) 
            {
                return cart.Product.BulkRate10;
            }
            else
            {
                return cart.Product.BulkRate100;
            }  
        }
    }
}
