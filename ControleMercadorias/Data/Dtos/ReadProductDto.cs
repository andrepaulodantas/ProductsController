using System.ComponentModel.DataAnnotations;

namespace Product.Data.Dtos
{
    public class ReadProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "O campo Nome deve conter apenas letras e números.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O campo Número de Registro é obrigatório.")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$", ErrorMessage = "O campo Número de Registro deve estar no formato XX.XXX.XXX/XXXX-XX.")]
        public string? RegistrationNumber { get; set; }

        public string? Manufacturer { get; set; }

        [Required(ErrorMessage = "O campo Tipo é obrigatório.")]
        public string? Type { get; set; }

        public string? Description { get; set; }

        public DateTime HoraConsulta { get; set; } = DateTime.Now; 
    }
}