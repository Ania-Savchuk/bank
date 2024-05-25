namespace BankApp.Models.Factories;

public interface ICardFactory
{
    ICard CreateCreditCard(Client.Client owner, double limit, double rate);
    ICard CreateDebitCard(Client.Client owner);
}
