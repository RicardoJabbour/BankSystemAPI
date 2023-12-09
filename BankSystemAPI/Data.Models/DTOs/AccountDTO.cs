using BankSystemAPI.Data.Models.Entities;

namespace BankSystemAPI.Data.Models.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public CustomerDTO? Customer { get; set; }
        public List<TransactionDTO> Transactions { get; set; }

    }
}
