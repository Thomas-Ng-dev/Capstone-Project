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

        public IActionResult IncrementQty(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get( x => x.Id == cartId );
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update( cartFromDb );
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult DecrementQty(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(x => x.Id == cartId);
            if(cartFromDb.Count <= 1) 
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(x => x.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
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
