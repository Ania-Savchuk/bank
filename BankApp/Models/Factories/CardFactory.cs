namespace BankApp.Models.Factories;

public class CardFactory : ICardFactory
{
    public ICard CreateCreditCard(Client.Client owner, double limit, double rate)
    {
        return new CreditCard(owner, limit, rate);
    }

    public ICard CreateDebitCard(Client.Client owner)
    {
        return new DebitCard(owner);
    }
}