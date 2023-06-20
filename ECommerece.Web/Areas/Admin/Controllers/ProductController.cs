using ECommerce.DataAccess.Service;
using ECommerce.Models;
using ECommerce.Repositories.IRepository;
using ECommerce.Repositories.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerece.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN,SUPER_ADMIN")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        private readonly IFileService _fileService;

        public ProductController(IUnitOfWork unitOfWork,
            IFileService fileService,
            ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var products = await _unitOfWork.Product.GetAll("Category");
                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching products: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {

            try
            {
                await GetCategoryList();

                //create
                if (id == null || id == 0)
                {

                    return View(new Product());
                }

                //Update
                var product = await _unitOfWork.Product.GetData(c => c.Id.Equals(id));
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in Upsert function: {ex.Message}");
                return View(new Product());
            }

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Product product, IFormFile? imageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (imageFile is not null)
                    {
                        var isImageUpload = _fileService.SaveImage(imageFile, "Product");
                        if (isImageUpload.Item1 == 0)
                        {
                            await GetCategoryList();
                            return View(product);
                        }
                        if (!string.IsNullOrWhiteSpace(product.ImageUrl))
                        {
                            _fileService.DeleteImage(product.ImageUrl, "Product");
                        }
                        product.ImageUrl = isImageUpload.Item2;
                    }

                    //create
                    if (product.Id == 0)
                    {
                        var isNameExist = await _unitOfWork.Product.IsValueExit(p => p.Name.Trim().ToLower().Equals(product.Name.Trim().ToLower()));
                        if (isNameExist)
                        {
                            ModelState.AddModelError("Name", "The Name Already Exist");
                            TempData["error"] = "The Name Already Exist";
                            await GetCategoryList();
                            return View(product);
                        }

                        product.CreatedAt = DateTime.Now;
                        await _unitOfWork.Product.Create(product);
                        TempData["success"] = "The Product created successfully!";
                    }
                    //update
                    else
                    {
                        var isNameExist = await _unitOfWork.Product.IsValueExit(p => p.Name.Trim().ToLower().Equals(product.Name.Trim().ToLower()) && p.Id != product.Id);
                        if (isNameExist)
                        {
                            ModelState.AddModelError("Name", "The Name Already Exist");
                            TempData["error"] = "The Name Already Exist";
                            await GetCategoryList();
                            return View(product);
                        }
                        await _unitOfWork.Product.Update(product);
                        TempData["success"] = "The Product updated successfully!";
                    }
                    return RedirectToAction("Index");
                }
                return View(product);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in Upsert post function: {ex.Message}");
                return View(product);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var product = await _unitOfWork.Product.GetData(p => p.Id.Equals(id));

                if (product is null)
                {
                    TempData["error"] = "The id is not valid!";
                    return RedirectToAction("Index");
                }

                await _unitOfWork.Product.Delete(product);
                TempData["success"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in delete function: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        private async Task GetCategoryList()
        {
            try
            {
                List<SelectListItem> categoryList = (await _unitOfWork.Category.GetAll())
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
                .ToList();
                ViewBag.categoryList = categoryList;

            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occurred in GetCategoryList function: {ex.Message}");
            }
        }

    }
}
