using AutoMapper;
using ControleMercadorias.Data;
using ControleMercadorias.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Product.Data.Dtos;


namespace ControleMercadorias.Controllers;


[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{

    private ApplicationDbContext _context;
    private IMapper _mapper;

    public ProductController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    //Adicionando DTO para criar um produto
    public void AddProduct([FromBody] CreateProductDto productDto)
    {
        ProductItem product = _mapper.Map<ProductItem>(productDto);
        if (!string.IsNullOrEmpty(product.Name) &&
            !string.IsNullOrEmpty(product.RegistrationNumber) &&
            !string.IsNullOrEmpty(product.Type))
        {

        //Adicionado para adicionar o produto no banco de dados
        _context.Products.Add(product);
        _context.SaveChanges();
        Console.WriteLine("Product added");
        }
        else
        {
            Console.WriteLine("Invalid product");
        }
    }
    
    [HttpGet]
    //Adcionando DTO para retornar os produtos
    public  IEnumerable<ReadProductDto> GetProducts([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        //Adicionado para retornar os produtos do banco de dados
        return _mapper.Map<List<ReadProductDto>>(_context.Products.Skip(skip).Take(take).ToList());
    } 

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        //Adicionado para retornar o produto do banco de dados
        //Adicionando DTO para retornar um produto ReadProductDto
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound("Product not found");
        var productDto = _mapper.Map<ReadProductDto>(product);
        return Ok(productDto);
    }
        

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        //Adicionado para deletar o produto do banco de dados
        var product = _context.Products.FirstOrDefault(p => p.Id == id);    
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product deleted");
        }
        else
        {
            return NotFound("Product not found");
        }
    }

    [HttpPut]
    public void UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
    {
        // Primeiro, carregue a entidade existente do banco de dados.
        var existingProduct = _context.Products.FirstOrDefault(p => p.Id == productDto.Id);

        if (existingProduct != null)
        {
            // Atualize a entidade existente com os dados do DTO.
            _mapper.Map(productDto, existingProduct);

            // Verifique se os campos obrigatórios estão preenchidos.
            if (!string.IsNullOrEmpty(existingProduct.Name) &&
                !string.IsNullOrEmpty(existingProduct.RegistrationNumber) &&
                !string.IsNullOrEmpty(existingProduct.Type))
            {
                // Salve as mudanças no banco de dados.
                _context.SaveChanges();
                Console.WriteLine("Product updated");
            }
            else
            {
                Console.WriteLine("Invalid product");
            }
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateProductPartial(int id, [FromBody] JsonPatchDocument<UpdateProductDto> productPatch)
    {
        // Primeiro, carregue a entidade existente do banco de dados.
        var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);

        if (existingProduct != null)
        {
            // Crie um DTO para armazenar as alterações.
            var productDto = _mapper.Map<UpdateProductDto>(existingProduct);

            // Aplique as alterações ao DTO.
            productPatch.ApplyTo(productDto, ModelState);

            // Verifique se os campos obrigatórios estão preenchidos.
            if (!string.IsNullOrEmpty(productDto.Name) &&
                !string.IsNullOrEmpty(productDto.RegistrationNumber) &&
                !string.IsNullOrEmpty(productDto.Type))
            {
                // Atualize a entidade existente com os dados do DTO.
                _mapper.Map(productDto, existingProduct);

                // Salve as mudanças no banco de dados.
                _context.SaveChanges();
                Console.WriteLine("Product updated");
                return Ok(existingProduct);
            }
            else
            {
                Console.WriteLine("Invalid product");
                return BadRequest("Invalid product");
            }
        }
        else
        {
            Console.WriteLine("Product not found");
            return NotFound("Product not found");
        }
    }

 /*    [HttpPost("entry")]
    public void AddProductEntry([FromBody] ProductEntry productEntry)
    {
        //Adicionado para adicionar a entrada do produto no banco de dados
        _context.ProductEntries.Add(productEntry);
        _context.SaveChanges();
        Console.WriteLine("Product entry added");
    }

    [HttpPost("exit")]
    public void AddProductExit([FromBody] ProductExit productExit)
    {
        //Adicionado para adicionar a saída do produto no banco de dados
        _context.ProductExits.Add(productExit);
        _context.SaveChanges();
        Console.WriteLine("Product exit added");
    }
 */
}

