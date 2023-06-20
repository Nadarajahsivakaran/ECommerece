using ECommerce.Models;
using ECommerce.Repositories.IRepository;
using ECommerece.Web.Areas.Customer.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerece.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN,SUPER_ADMIN")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork unitOfWork, ILogger<CategoryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _unitOfWork.Category.GetAll();
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching categories: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                //create
                if (id == null || id == 0)
                {
                    return View(new Category());
                }

                //Update
                var category = await _unitOfWork.Category.GetData(c => c.Id.Equals(id));
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in Upsert function: {ex.Message}");
                return View(new Category());
            }

        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //create
                    if (category.Id == 0)
                    {
                        var isNameExist = await _unitOfWork.Category.IsValueExit(c => c.Name.Trim().ToLower().Equals(category.Name.Trim().ToLower()));
                        if (isNameExist)
                        {
                            ModelState.AddModelError("Name", "The Name Already Exist");
                            TempData["error"] = "The Name Already Exist";
                            return View(category);
                        }
                        await _unitOfWork.Category.Create(category);
                        TempData["success"] = "The Category created successfully!";
                    }
                    //update
                    else
                    {
                        var isNameExist = await _unitOfWork.Category.IsValueExit(c => c.Name.Trim().ToLower().Equals(category.Name.Trim().ToLower()) && c.Id != category.Id);
                        if (isNameExist)
                        {
                            ModelState.AddModelError("Name", "The Name Already Exist");
                            TempData["error"] = "The Name Already Exist";
                            return View(category);
                        }
                        await _unitOfWork.Category.Update(category);
                        TempData["success"] = "The Category updated successfully!";
                    }
                    return RedirectToAction("Index");
                }
                return View(category);

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in Upsert post function: {ex.Message}");
                return View(category);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var category = await _unitOfWork.Category.GetData(c => c.Id.Equals(id));

                if (category is null)
                {
                    TempData["error"] = "The id is not valid!";
                    return RedirectToAction("Index");
                }

                await _unitOfWork.Category.Delete(category);
                TempData["success"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred in delete function: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

    }
}

