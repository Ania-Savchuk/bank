namespace BankApp.Models.Account;

public static class AccountData
{
    public static IList<Account> DatabaseStub { get; }= [];
    //? означає, що вона може бути null 
    public static Account? CurrentAccount { get; set; }
}