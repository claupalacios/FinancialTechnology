namespace FinancialTechnology.Dtos
{
    public class AccountDto
    {
        public int OwnerId { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
