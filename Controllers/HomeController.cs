using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Diagnostics;
using SportsStore.Models.ViewModels;
namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStoreRepository repository;
        private int PageSize = 4;
        public HomeController(ILogger<HomeController> logger, IStoreRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public IActionResult Index(string? category,int ProductPage = 1)
        {
            return View(new ProductListViewModel
            {
                Products =repository.Products
                                    .Where(p => p.Category == category || category == null)
                                    .OrderBy(x => x.ProductId)
                                     .Skip((ProductPage- 1) * PageSize)
                                     .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage= ProductPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products
                                            .Where(p => p.Category == category || category == null)
                                            .Count()
                },
                CurrentCategory= category
            });

        }

        
    }
}