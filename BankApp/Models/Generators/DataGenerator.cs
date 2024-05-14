namespace BankApp.Models.Generators;

public static class DataGenerator
{
    private static readonly Random Rand = Random.Shared;

    public static string GenerateCardNumber()
    {
        const int cardNumberLength = 16;
        string cardNumber = "";
        for (int i = 0; i < cardNumberLength; i++)
        {
            if (i > 0 && i % 4 == 0)
            {
                cardNumber += " ";
            }
            int digit = Rand.Next(10);
            cardNumber += digit;
        }
        return cardNumber;
    }

    public static string GenerateIban()
    {
        const int ibanLength = 27;
        string iban = "UA";
        for (int i = 0; i < ibanLength; i++)
        {
            int digit = Rand.Next(10);
            iban += digit;
        }
        return iban;
    }

    public static string GeneratePassword()
    {
        const int passwordLength = 4;
        string password = "";
        for (int i = 0; i < passwordLength; i++)
        {
            int digit = Rand.Next(10);
            password += digit;
        }
        return password;
    }
}