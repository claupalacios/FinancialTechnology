using System.ComponentModel.DataAnnotations;

namespace FinancialTechnology.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public User Owner { get; set; }
    }
}
