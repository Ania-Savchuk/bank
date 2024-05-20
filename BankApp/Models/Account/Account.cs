namespace BankApp.Models.Account;

public class Account
{
    private static long _lastUsedId = 0;
    //init - дозволяє встановлювати значення лише під час ініціалізації об'єкта.
    public long Id { get; init; }
    public Client.Client Client { get; init; }
    public string PhoneNumber { get; init; }
    // private readonly string _phoneNumber;
    private readonly IList<ICard> _myCard;

    public Account() : this(new Client.Client(), String.Empty) {}
    public Account(Client.Client user, string phoneNumber)
    {
        _lastUsedId += 1;
        Id = _lastUsedId;
        Client = user; 
        PhoneNumber = phoneNumber;
        Console.WriteLine("The account successfully registered!");
        _myCard = [];
    }

    public override string ToString()
    {
        StringBuilder bld = new();
        bld.Append("Client Information:");
        bld.Append($"First Name: {Client.FirstName}");
        bld.Append($"Last Name: {Client.LastName}");
        bld.Append($"Age: {Client.Age}");
        bld.Append($"Phone_number: {PhoneNumber}\n");
        return bld.ToString();
    }

    public void AddCard(ICard card) => _myCard.Add(card);

    public bool DepositMoney(int num, double amount)
    {
        if (num < 1 || num > _myCard.Count)
        {
            return false;
        }

        return _myCard[num - 1].Deposit(amount);
    }

    public bool WithdrawMoney(int num, double amount)
    {
        if (num < 1 || num > _myCard.Count)
        {
            return false;
        }

        return _myCard[num - 1].Withdraw(amount);
    }

    public Transfer.TransferStatus TransferMoney(int from, int to, double amount)
    {
        if (from < 1 || from > _myCard.Count || to < 1 || to > _myCard.Count)
        {
            return Transfer.TransferStatus.WrongCardNumber;
        }

        Transfer transfer = new(_myCard[from - 1], _myCard[to - 1], amount);
        return transfer.ExecuteTransfer();
    }


    public List<ICard> GetCards()
    {
        // Return the list of cards associated with this account
        return new List<ICard>(_myCard);
    }
}

