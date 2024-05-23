namespace la_mia_pizzeria_crud_mvc.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Pizza> Pizzas { get; set; }
    }
}
