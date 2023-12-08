namespace BankSystemAPI.Data.Models.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
