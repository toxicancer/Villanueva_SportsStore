using Microsoft.AspNetCore.Mvc;
using Villanueva_SportsStore.Models;

namespace Villanueva_SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository; // you can also use underscores (e.g. _repository)
        public int pageSize = 4;

        public HomeController(IStoreRepository repository)
        {
            this.repository = repository; // make sure naka match siya
        }

        public ViewResult Index( int productPage = 1) // website.com/?productPage=2
            => View(repository.Products
            .OrderBy(p => p.ProductID)
            .Skip((productPage - 1) * pageSize)
            .Take(pageSize));
        
    }
}
