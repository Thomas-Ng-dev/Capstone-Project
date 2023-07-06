using Capstone.DataAccess.Repository.IRepository;
using Capstone.Models;
using Capstone.Models.ViewModels;
using Capstone.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

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
			ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(x => x.Id == userId);

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
            // Stripe code
			// Replace localhost URL if running on different machine
			var domain = "https://localhost:7055/";
			var options = new SessionCreateOptions
            {	
				SuccessUrl = domain + $"Client/ShoppingCart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
				LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment",
			};
            foreach(var item in ShoppingCartVM.ShoppingCartList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.ItemPrice * 100),
                        Currency = "cad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
                        }


                    },
                    Quantity = item.Count
                };
				options.LineItems.Add(sessionLineItem);
			}

			var service = new SessionService();
			service.Create(options);
            Session session = service.Create(options);
            _unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

            //return RedirectToAction("OrderConfirmation", new { id = ShoppingCartVM.OrderHeader.Id });
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
