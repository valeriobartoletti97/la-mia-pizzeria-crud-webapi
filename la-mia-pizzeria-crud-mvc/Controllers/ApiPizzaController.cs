using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class ApiPizzaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
