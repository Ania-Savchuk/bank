namespace BankApp.Models.Account;

public static class AccountData
{
    public static IList<Account> DatabaseStub { get; }= [];
    public static Account? CurrentAccount { get; set; }
}