namespace BankApp.Models.Card;

public class DebitCard(Client.Client owner) : Card(owner, 0.0)
{
    public override bool Deposit(double amount)
    {
        if (amount < 0)
        {
            return false;
        }

        Balance += amount;
        return true;
    }

    public override bool Withdraw(double amount)
    {
        if (!(amount > 0) || !(amount <= Balance))
        {
            return false;
        }

        Balance -= amount;
        return true;
    }
}