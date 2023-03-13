using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        public IStoreRepository repo;


        public CartModel(IStoreRepository repo,Cart cartService)
        {
            this.repo = repo;
            this.Cart= cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            ReturnUrl= returnUrl ?? "/";
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(long productId , string returnUrl ) 
        {
            Product? product = repo.Products.FirstOrDefault(p => p.ProductId == productId);
            if(product != null)
            {
                //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart ();
                Cart.AddItem(product, 1);
                //HttpContext.Session.SetJson("cart", Cart);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(long productId , string returnUrl)
        {
            
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
            return RedirectToPage(new {returnUrl= returnUrl});
        }
    }
}
