using AutoMapper;
using BankSystemAPI.Data.Models.DTOs;
using BankSystemAPI.Data.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankSystemAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Account, AccountDTO>();
            CreateMap<Transaction, TransactionDTO>();

            CreateMap<CustomerDTO, Customer>();
            CreateMap<AccountDTO, Account>();
            CreateMap<TransactionDTO, Transaction>();
        }
    }
}
