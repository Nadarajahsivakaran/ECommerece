using ECommerce.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerece.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Product.GetAll("Category");
            return View(products);
        }
    }
}
