namespace BankApp.Models.Card;

public class CardManager
{
    public static ICard? CreateCard(Client.Client client)
    {
        Console.Write("Choose card type (1 - Credit Card, 2 - Debit Card): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
            {
                Console.Write("Enter credit limit: ");
                double limit = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter interest rate: ");
                double rate = Convert.ToDouble(Console.ReadLine());
                return new CreditCard(client, limit, rate);
            }
            case 2:
                return new DebitCard(client);
            default:
                Console.WriteLine("Invalid choice.");
                return null;
        }
    }
}