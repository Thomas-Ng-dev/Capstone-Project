using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Capstone.Utility;
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
        [BindProperty]
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
                    includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach(var item in ShoppingCartVM.ShoppingCartList)
            {
                item.ItemPrice = GetQuantityPrice(item);
                ShoppingCartVM.OrderHeader.OrderTotal += (item.ItemPrice * item.Count);
            }
            return View(ShoppingCartVM);
        }
        public IActionResult ShoppingCartSummary()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    x => x.ApplicationUserId == userId,
                    includeProperties: "Product"),
                OrderHeader = new()
            };

            // This page needs OrderHeader to be populated
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(x => x.Id == userId);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.Province = ShoppingCartVM.OrderHeader.ApplicationUser.Province;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;


            foreach (var item in ShoppingCartVM.ShoppingCartList)
            {
                item.ItemPrice = GetQuantityPrice(item);
                ShoppingCartVM.OrderHeader.OrderTotal += (item.ItemPrice * item.Count);
            }
            return View(ShoppingCartVM);
        }
        [HttpPost]
        [ActionName("ShoppingCartSummary")]
        public IActionResult ShoppingCartSummaryPOST()
        {
			var identity = (ClaimsIdentity)User.Identity;
			var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId,
                    includeProperties: "Product");
            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
			ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(x => x.Id == userId);

            ShoppingCartVM.OrderHeader.PaymentStatus = StaticDetails.PaymentPending;
			ShoppingCartVM.OrderHeader.OrderStatus = StaticDetails.StatusPending;

			foreach (var item in ShoppingCartVM.ShoppingCartList)
			{
				item.ItemPrice = GetQuantityPrice(item);
				ShoppingCartVM.OrderHeader.OrderTotal += (item.ItemPrice * item.Count);
			}

			_unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();
            foreach(var item in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = item.ItemPrice,
                    Count = item.Count
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }
			return View(ShoppingCartVM);
		}

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
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
