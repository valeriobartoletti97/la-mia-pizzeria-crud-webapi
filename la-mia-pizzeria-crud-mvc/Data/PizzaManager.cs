using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_crud_mvc.Data
{
    public static class PizzaManager
    {
        public static int CountPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizzas.Count();
        }
        public static List<Pizza> GetAllPizza()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizzas.ToList();
        }

        public static Pizza GetPizza(int id, bool WithCategories = true)
        {
            using PizzaContext db = new PizzaContext();
            if(WithCategories)
                return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();
            return db.Pizzas.FirstOrDefault(p => p.Id == id);
        }

        public static void AddPizza(Pizza pizza,List<string> selectedIngredients)
        {
            using PizzaContext db = new PizzaContext();
            pizza.Ingredients = new List<Ingredient>();
            if(selectedIngredients != null)
            {
                foreach (var ingredient in selectedIngredients)
                {
                    int id = int.Parse(ingredient);
                    var ingredientDB = db.Ingredients.FirstOrDefault(p => p.Id == id);
                    if(ingredientDB != null)
                    {
                        pizza.Ingredients.Add(ingredientDB);
                    }
                }
            }
            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        //UPDATE CON LAMBDA
        //public static bool UpdatePizza(int id, Action<Pizza> edit)
        //{
        //    using PizzaContext db = new PizzaContext();
        //    var pizza = db.Pizzas.FirstOrDefault(p => p.Id == id);

        //    if (pizza == null)
        //        return false;

        //    edit(pizza);

        //    db.SaveChanges();

        //    return true;
        //}

        public static bool UpdatePizza(int id, Pizza pizza, List<string> selectedIngredients)
        {
            try
            {
                using PizzaContext db = new PizzaContext();
                var pizzaModificata = db.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();
                if (pizzaModificata == null)
                    return false;
                pizzaModificata.Name = pizza.Name;
                pizzaModificata.Description = pizza.Description;
                pizzaModificata.Price = pizza.Price;
                pizzaModificata.CategoryId = pizza.CategoryId;

                pizzaModificata.Ingredients.Clear();
                if (selectedIngredients != null)
                {
                    foreach (var ingredient in selectedIngredients)
                    {
                        int ingredientId = int.Parse(ingredient);
                        var ingredientFromDb = db.Ingredients.FirstOrDefault(x => x.Id == ingredientId);
                        if (ingredientFromDb != null)
                            pizzaModificata.Ingredients.Add(ingredientFromDb);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

            public static bool DeletePizza(int id)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = PizzaManager.GetPizza(id);

            if (pizza == null)
                return false;

            db.Pizzas.Remove(pizza);
            db.SaveChanges();

            return true;
        }
        public static List<Category> GetAllCategories()
        {
            using PizzaContext db = new PizzaContext();
            return db.Categories.ToList();
        }
        public static List<Ingredient> GetAllIngredients()
        {
            using PizzaContext db = new PizzaContext();
            return db.Ingredients.ToList();
        }

        public static void Seed()
        {
            if(PizzaManager.CountPizzas() == 0)
            {
               PizzaManager.AddPizza(new Pizza("Bufala", "Pomodoro, Mozzarella di Bufala", "bufala.jfif", 10),new());
               PizzaManager.AddPizza(new Pizza("Diavola", "Pomodoro, Mozzarella di Bufala, Salame", "diavola.jpg", 8), new());
               PizzaManager.AddPizza(new Pizza("Funghi", "Pomodoro, Funghi", "funghi.jfif", 8), new());
               PizzaManager.AddPizza(new Pizza("Zucchine", "Zucchine, Mozzarella", "zucchine.jfif", 9), new());
               PizzaManager.AddPizza(new Pizza("Boscaiola", "Pomodoro, Mozzarella, Funghi", "boscaiola.jpg", 11), new());
            }
        }
        //public static List<Pizza> ListPizza = new List<Pizza>();
        //public static Pizza CreatePizza(string name, string description, string image, decimal price)
        //{
        //    Pizza pizza = new Pizza(name, description, image, price);
        //    return pizza;
        //}
        //public static void AddPizza(Pizza pizza)
        //{
        //    ListPizza.Add(pizza);
        //}
    }
}
