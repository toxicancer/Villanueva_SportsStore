using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Villanueva_SportsStore.Infrastructure;
using Villanueva_SportsStore.Models;

namespace Villanueva_SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository repository;
        public CartModel(IStoreRepository repo)
        {
            repository = repo;
        }
        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet()
        {
            ReturnUrl = ReturnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
		    if(product != null){
		        Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
