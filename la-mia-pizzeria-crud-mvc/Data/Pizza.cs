using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.Data
{
    public class Pizza
    {
        [Key] public int Id { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Il nome deve contenere tra i 3 e i 30 caratteri")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "La descrizione deve contenere tra i 5 e i 1000 caratteri")]
        public string Description { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public Pizza(string name, string description, string image, decimal price)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }

        public Pizza()
        {

        }
    }
}
