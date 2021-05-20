using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShowcaseController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ShowcaseController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("Showcase")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }
        
        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _productAppService.GetById(id));
        }
    }
}