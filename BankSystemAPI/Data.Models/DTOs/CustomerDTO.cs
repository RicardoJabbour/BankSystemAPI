using System.Text.Json.Serialization;

namespace BankSystemAPI.Data.Models.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public List<AccountDTO> Accounts { get; set; }
    }
}
