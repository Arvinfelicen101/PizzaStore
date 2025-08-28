using System.ComponentModel.DataAnnotations;

namespace PizzaStore.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
    }
}
