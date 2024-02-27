
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleMercadorias.Models
{
    public class ProductEntry
    {
        [Key]
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }


        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O campo Quantidade deve ser maior que 0.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "O campo Quantidade deve conter apenas números.")]
        public int Quantity { get; set; }


        [Required]
        //Gerar data e hora atual automaticamente
        [DataType(DataType.DateTime)]            
        public DateTime DateTime { get; set; }


        [Required]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$", ErrorMessage = "O campo Número de Registro deve estar no formato XX.XXX.XXX/XXXX-XX.")]
        public string? Location { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public ProductItem? Product { get; set; }
    }
}
