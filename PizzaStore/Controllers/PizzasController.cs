using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.DTOs;
using PizzaStore.Models;

namespace PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaDbContext _context;
        public PizzasController(PizzaDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaDto>>> GetPizzas()
        {
            var pizzas = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .ToListAsync();

            var result = pizzas.Select(p => new PizzaDto
            {
                Id = p.Id,
                Name = p.Name,
                Toppings = p.PizzaToppings.Select(pt => pt.Topping.Name).ToList()
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PizzaDto>> CreatePizza(CreatePizzaDto dto)
        {
            var pizza = new Pizza { Name = dto.Name };

            foreach (var toppingId in dto.ToppingIds)
            {
                pizza.PizzaToppings.Add(new PizzaTopping
                {
                    ToppingId = toppingId
                });
            }

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            var result = new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Toppings = await _context.PizzaToppings
                    .Where(pt => pt.PizzaId == pizza.Id)
                    .Select(pt => pt.Topping.Name)
                    .ToListAsync()
            };

            return CreatedAtAction(nameof(GetPizzas), new { id = pizza.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PizzaDto>> UpdatePizza(int id, CreatePizzaDto dto)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null)
                return NotFound();

            pizza.Name = dto.Name;

            pizza.PizzaToppings.Clear();

            foreach (var toppingId in dto.ToppingIds)
            {
                pizza.PizzaToppings.Add(new PizzaTopping
                {
                    PizzaId = id,
                    ToppingId = toppingId
                });
            }

            await _context.SaveChangesAsync();

            
            var result = new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Toppings = await _context.PizzaToppings
                    .Where(pt => pt.PizzaId == pizza.Id)
                    .Select(pt => pt.Topping.Name)
                    .ToListAsync()
            };

            return Ok(result);
        }


     
        [HttpPut("{id}/toppings")]
        public async Task<IActionResult> UpdateToppings(int id, [FromBody] UpdateToppingsDto dto)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pizza == null) return NotFound();

        
            pizza.PizzaToppings.Clear();

         
            foreach (var tid in dto.ToppingIds)
                pizza.PizzaToppings.Add(new PizzaTopping { PizzaId = id, ToppingId = tid });

            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null) return NotFound();

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
