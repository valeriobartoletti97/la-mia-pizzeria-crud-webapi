using la_mia_pizzeria_crud_mvc.Data;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles ="Admin,User")]
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizza());
        }

        [Authorize(Roles = "Admin,User")]

        public IActionResult GetPizza(int id)
        {
            return View(PizzaManager.GetPizza(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext context = new PizzaContext())
            {
                PizzaFormModel  model= new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categories = PizzaManager.GetAllCategories();
                model.CreateIngredients();
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                // Ritorniamo "data" alla view così che la form abbia di nuovo i dati inseriti
                // (anche se erronei)
                data.Categories = PizzaManager.GetAllCategories();
                data.CreateIngredients();
                return View("Create", data);
            }

            PizzaManager.AddPizza(data.Pizza, data.SelectedIngredients);
            //using (PizzaContext db = new PizzaContext())
            //{
            //    var pizza = new Pizza(data.Name, data.Description, data.Image, data.Price);
            //    db.Pizzas.Add(pizza);
            //    db.SaveChanges();
            //}
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza pizzaToEdit = PizzaManager.GetPizza(id);

                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {

                    PizzaFormModel model = new PizzaFormModel(pizzaToEdit, PizzaManager.GetAllCategories());
                    model.CreateIngredients();
                    return View(model);
                }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories();
                data.CreateIngredients();
                return View("Update", data);
            }
            var modified = PizzaManager.UpdatePizza(id, data.Pizza, data.SelectedIngredients);
            if (modified)
            {
                // Richiamiamo la action Index affinché vengano mostrate tutte le pizze
                return RedirectToAction("Index");
            }
            else
                return NotFound();

            //Modifica con lambda function
            //bool result = PizzaManager.UpdatePizza(id, pizza =>
            //{
            //    pizza.Name = data.Pizza.Name;
            //    pizza.Description = data.Pizza.Description;
            //    pizza.Image = data.Pizza.Image;
            //    pizza.Price = data.Pizza.Price;
            //    pizza.CategoryId = data.Pizza.CategoryId;
            //});
            //if (result)
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return NotFound();
            //}

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (PizzaManager.DeletePizza(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
