using System.Collections.Generic;

namespace PizzaStore.DTOs
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Toppings { get; set; } = new();
    }
}
