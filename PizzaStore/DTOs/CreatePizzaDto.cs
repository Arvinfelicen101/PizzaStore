namespace PizzaStore.DTOs
{
    public class CreatePizzaDto
    {
        public string Name { get; set; }
        public List<int> ToppingIds { get; set; } = new();
    }
}
