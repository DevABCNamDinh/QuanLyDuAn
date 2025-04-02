using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyDuAn.Service;

namespace QuanLyDuAn.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Cart()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
            var cartItems = _cartService.GetCartItems(cart.Id);
            return View(cartItems);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(Guid bookId, int quantity)
        {
            var userId = HttpContext.Session.GetString("UserId");
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return RedirectToAction("Login", "User");
            //}
            try
            {
                _cartService.AddToCart(Guid.Parse(userId), bookId, quantity);
                TempData["Message"] = $"{quantity} cuốn sách đã được thêm vào giỏ hàng";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Details", "Book", new { id = bookId });
        }

        [HttpPost]
        public IActionResult UpdateCartItem(Guid cartItemId, int quantity)
        {
            try
            {
                _cartService.UpdateCartItem(cartItemId, quantity);
                TempData["Message"] = "Giỏ hàng đã được cập nhật";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveCartItem(Guid cartItemId)
        {
            _cartService.RemoveCartItem(cartItemId);
            TempData["Message"] = "Sách đã được xóa";
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
            if (cart != null)
            {
                _cartService.ClearCart(cart.Id);
                TempData["Message"] = "Giỏ hàng đã được xóa";
            }
            return RedirectToAction("Cart");
        }
    }
}
