using Microsoft.AspNetCore.Mvc;
using Villanueva_SportsStore.Models;
using Villanueva_SportsStore.Models.ViewModels;

namespace Villanueva_SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository; // you can also use underscores (e.g. _repository)
        public int PageSize = 4;

        public HomeController(IStoreRepository repository)
        {
            this.repository = repository; // make sure naka match siya
        }

        public ViewResult Index(string? category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
					TotalItems = category == null
                        ? repository.Products.Count()
                        : repository.Products.Where(e =>
                            e.Category == category).Count()
				},
                CurrentCategory = category
            });
    }
}
