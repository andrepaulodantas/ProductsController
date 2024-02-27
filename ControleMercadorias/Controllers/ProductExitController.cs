using ControleMercadorias.Data;
using ControleMercadorias.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ControleMercadorias.Controllers;

[ApiController]
[Route("[controller]")]

    public class ProductExitController : ControllerBase
    {
        private  ApplicationDbContext _context;

        public ProductExitController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        
        // Método para a criação de uma nova entrada de produto
        [HttpGet]
        public IEnumerable <ProductExit> GetExits([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _context.ProductExits.Skip(skip).Take(take);
        }
       

        [HttpPost]
        public IActionResult AddExit([FromBody] ProductExit exit)
        {
            //gerar data e hora automaticamente
            exit.DateTime = DateTime.Now;
            _context.ProductExits.Add(exit);
            _context.SaveChanges();
            Console.WriteLine("Product exit");
            return CreatedAtAction(nameof(GetExits), new { id = exit.Id }, exit);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExit(int id)
        {
            var exit = _context.ProductExits.Find(id);
            if (exit == null)
            {
                return NotFound();
            }
            _context.ProductExits.Remove(exit);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExit(int id, [FromBody] ProductExit exit)
        {
            if (id != exit.Id)
            {
                return BadRequest();
            }
            _context.Entry(exit).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }
    }




