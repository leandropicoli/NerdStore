using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Application.ViewModel;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }
        
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var product = await _productAppService.GetById(id);
            return View(await HandleCategories(product));
        }
        
        [Route("update-product")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productAppService.GetById(id);
            productViewModel.InventoryAmount = product.InventoryAmount;

            ModelState.Remove("InventoryAmount");
            if (!ModelState.IsValid) return View(await HandleCategories(productViewModel));
            
            await _productAppService.UpdateProduct(productViewModel);
            return RedirectToAction("Index");
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await HandleCategories(new ProductViewModel()));
        }
        
        [Route("new-product")]
        [HttpPost]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(await HandleCategories(productViewModel));

            await _productAppService.AddProduct(productViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-inventory")]
        public async Task<IActionResult> UpdateInventory(Guid id)
        {
            return View("Inventory", await _productAppService.GetById(id));
        }
        
        [HttpPost]
        [Route("products-update-inventory")]
        public async Task<IActionResult> UpdateInventory(Guid id, int amount)
        {
            if (amount > 0)
            {
                await _productAppService.IncreaseInventory(id, amount);
            }
            else
            {
                await _productAppService.DecreaseInventory(id, amount);
            }
            
            return View("Index", await _productAppService.GetAll());
        }

        private async Task<ProductViewModel> HandleCategories(ProductViewModel productViewModel)
        {
            productViewModel.Categories = await _productAppService.GetAllCategories();

            return productViewModel;
        }
    }
}