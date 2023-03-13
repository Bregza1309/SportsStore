using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
namespace SportsStore.Components
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        public IStoreRepository repository;
        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["cat"] = RouteData?.Values["category"];
            return View(repository.Products.Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x));
        }
        
    }
}
