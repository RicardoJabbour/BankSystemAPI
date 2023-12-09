using System.ComponentModel;

public enum TransactionType
{
    [Description("Withdrawal")]
    Withdraw = 1,
    [Description("Deposit")]
    Deposit = 2,
    [Description("Transfer")]
    Transfer = 3
}