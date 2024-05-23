using la_mia_pizzeria_crud_mvc.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Xml.Linq;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaApiController : ControllerBase  //RESTITUISCE TUTTE LE PIZZE NEL DB
    {
        [HttpGet]
        public IActionResult GetPizzas(string? name) // .../GetPizzas?name=<<name>>
        {
            if (name != null) 
            {
                List<Pizza> listaPizze = PizzaManager.GetAllPizza().Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
                if (listaPizze.Count > 0)
                    return Ok(listaPizze);
                return NotFound($"Errore la pizza {name} non è stata trovata!");
            }
            return Ok(PizzaManager.GetAllPizza());
        }

        [HttpGet]
        public IActionResult GetPizza(int id) // ...GetPizza?id=<<id>>
        {
            Pizza pizza = PizzaManager.GetPizza(id);
            if (pizza == null) 
            { 
                return NotFound("ERRORE! LA PIZZA NON E' STATA TROVATA");
            }
            return Ok(pizza);
        }

        [HttpPost]   
        public IActionResult CreatePizza([FromBody] Pizza data)
        {
            PizzaManager.AddPizza(data, null);
            return Ok($"La pizza \"{data.Name}\" è stata creata!");
        }

        [HttpPut] 
        public IActionResult UpdatePizza(int id, [FromBody] Pizza data) // ...UpdatePizza?id=<<id>>
        {
            var PizzaDaModificare = PizzaManager.GetPizza(id);
            if (PizzaDaModificare == null)
                return NotFound($"Errore la pizza {data.Name} non è stata trovata!");
            return Ok(data);
        }

        [HttpDelete]
        public IActionResult DeletePizza(int id) // ...UpdatePizza?id=<<id>>
        {
            Pizza pizza = PizzaManager.GetPizza(id);
            if (pizza == null)
            {
                return NotFound("ERRORE! LA PIZZA NON E' STATA TROVATA");
            }

             var isDeleted = PizzaManager.DeletePizza(id);
            if (!isDeleted)
                return NotFound("Non è stato possibile eliminare la pizza");
            return Ok($"La pizza {pizza.Name} è stata eliminata con successo");
        }
    }


}
