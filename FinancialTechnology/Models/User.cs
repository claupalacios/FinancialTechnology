using System.ComponentModel.DataAnnotations;

namespace FinancialTechnology.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
