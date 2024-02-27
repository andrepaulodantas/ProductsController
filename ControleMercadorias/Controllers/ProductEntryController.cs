using ControleMercadorias.Data;
using ControleMercadorias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadorias.Controllers;

[ApiController]
[Route("[controller]")]

    public class ProductEntryController : ControllerBase
    {
        private  ApplicationDbContext _context;

        public ProductEntryController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        
        // Método para a criação de uma nova entrada de produto
        [HttpGet]
        public IEnumerable <ProductEntry> GetEntries([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _context.ProductEntries.Skip(skip).Take(take);
        }
       

        [HttpPost]
        public IActionResult AddEntry([FromBody] ProductEntry entry)
        {
            //gerar data e hora automaticamente
            entry.DateTime = DateTime.Now;
            _context.ProductEntries.Add(entry);
            _context.SaveChanges();
            Console.WriteLine("Product entry added");
            return CreatedAtAction(nameof(GetEntries), new { id = entry.Id }, entry);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var entry = _context.ProductEntries.Find(id);
            if (entry == null)
            {
                return NotFound();
            }
            _context.ProductEntries.Remove(entry);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEntry(int id, [FromBody] ProductEntry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }
            _context.Entry(entry).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }
    }


