using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
using PizzaStore.DTOs;

namespace PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly PizzaDbContext _context;
        public ToppingsController(PizzaDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingDto>>> GetAll()
        {
            var toppings = await _context.Toppings
                .Select(t => new ToppingDto { Id = t.Id, Name = t.Name })
                .ToListAsync();

            return toppings;
        }

        [HttpPost]
        public async Task<ActionResult<ToppingDto>> Create(CreateToppingDto dto)
        {
            if (await _context.Toppings.AnyAsync(t => t.Name.ToLower() == dto.Name.ToLower()))
                return BadRequest(new { message = "Topping name already exists." });

            var topping = new Topping { Name = dto.Name };

            _context.Toppings.Add(topping);
            await _context.SaveChangesAsync();

            var result = new ToppingDto { Id = topping.Id, Name = topping.Name };

            return CreatedAtAction(nameof(GetAll), new { id = topping.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateToppingDto dto)
        {
            var existing = await _context.Toppings.FindAsync(id);
            if (existing == null) return NotFound();

            if (await _context.Toppings.AnyAsync(t => t.Name.ToLower() == dto.Name.ToLower() && t.Id != id))
                return BadRequest(new { message = "Topping name already exists." });

            existing.Name = dto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topping = await _context.Toppings.FindAsync(id);
            if (topping == null) return NotFound();

            _context.Toppings.Remove(topping);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
