using System.ComponentModel.DataAnnotations;

namespace FinancialTechnology.Dtos
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
