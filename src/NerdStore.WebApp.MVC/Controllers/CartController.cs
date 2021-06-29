using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CartController : Controller
    {
        [HttpPost]
        public IActionResult AddItem()
        {
            return Ok();
        }
    }
}