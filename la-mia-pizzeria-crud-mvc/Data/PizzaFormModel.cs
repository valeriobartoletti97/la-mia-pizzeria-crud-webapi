using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_mvc.Data
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredients { get; set; }


        public PizzaFormModel() { }

        public PizzaFormModel(Pizza pizza, List<Category>? categories)
        {
            Pizza = pizza;
            Categories = categories;
            SelectedIngredients = new List<string>();
            if (Pizza.Ingredients != null)
            {
                foreach (var i in  Pizza.Ingredients)
                    SelectedIngredients.Add(i.Id.ToString());
            }
        }
        public void CreateIngredients()
        {
            this.Ingredients = new List<SelectListItem>();
            if(this.SelectedIngredients == null)
            {
                this.SelectedIngredients = new List<string>();
            }
            var ingredientsDB = PizzaManager.GetAllIngredients();
            foreach(var ing in ingredientsDB)
            {
                bool selected = this.SelectedIngredients.Contains(ing.Id.ToString());
                this.Ingredients.Add(new SelectListItem()
                {
                    Text = ing.Name,
                    Value = ing.Id.ToString(),
                    Selected = selected
                });
                //if (selected )
                //{
                //    this.SelectedIngredients.Add(ing.Id.ToString());
                //}
            }
        }

    }
}
