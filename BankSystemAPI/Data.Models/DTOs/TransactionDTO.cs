using BankSystemAPI.Data.Models.Entities;
using System.Transactions;

namespace BankSystemAPI.Data.Models.DTOs
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType  TransactionType { get; set; }
        public AccountDTO Account { get; set; }

    }
}
