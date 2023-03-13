using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repo;
        private Cart cart;
        public OrderController(IOrderRepository repo, Cart cart)
        {
            this.repo = repo;
            this.cart = cart;
        }
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if(!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry your cart is empty");
            }
            if(ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repo.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed",new { orderId = order.OrderId});
            }
            else
            {
                return View();
            }
        }
        
    }
}
