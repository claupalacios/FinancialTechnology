using System.ComponentModel.DataAnnotations;

namespace FinancialTechnology.Dtos
{
    public class AccountDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }
}
