namespace BankApp.Models.Card;

public interface ICard
{
    bool Withdraw(double amount);
    bool Deposit(double amount);
    double Balance { get; }
    string CardNumber { get; }
}