using ECommerce.Repositories.IRepository;
using ECommerece.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerece.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork,ILogger<CartController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveCart()
        {
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occurred while fetching products: {ex.Message}");
                return View();
            }
            
        }

    }
}
