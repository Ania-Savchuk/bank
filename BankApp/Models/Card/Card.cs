namespace BankApp.Models.Card;

public abstract class Card(Client.Client owner, double balance) : ICard
{
    public string CardNumber { get; init; } = DataGenerator.GenerateCardNumber();
    public string Password { get; init; } = DataGenerator.GeneratePassword();
    public string Iban { get; init; } = DataGenerator.GenerateIban();

    public virtual double Balance { get; protected set; } = balance;

    // public virtual double GetBalance()
    // {
    //     return balance;
    // }
    //
    // public virtual void SetBalance(double newBalance)
    // {
    //     balance = newBalance;
    // }

    public override string ToString()
    {
        StringBuilder bld = new();
        bld.Append($"Card Number: {CardNumber}");
        bld.Append($"Password: {Password}");
        bld.Append($"IBAN: {Iban}");
        bld.Append($"Balance: {Balance} USD");
        bld.Append($"Owner: {owner.FirstName} {owner.LastName}");
        return bld.ToString();
    }

    public virtual bool Deposit(double amount)
    {
        // inverted if
        if (amount <= 0) return false;
        
        double newBalance = Balance + amount;
        Balance = newBalance;

        return true;
    }

    public virtual bool Withdraw(double amount)
    {
        if (amount <= 0|| amount > Balance)
        {
            return false;
        }

        double newBalance = Balance - amount;
        Balance = newBalance;
        return true;
    }
}