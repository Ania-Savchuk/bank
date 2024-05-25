namespace BankApp.Models.Factories;

public class CardFactorySingleton
{
    private static readonly Lazy<ICardFactory> instance = new Lazy<ICardFactory>(() => new CardFactory());

    public static ICardFactory Instance => instance.Value;

    private CardFactorySingleton() { }
}
