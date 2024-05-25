namespace BankApp.Models.TransferData;

public class TransferViewModel
{
    public long AccountId { get; init; }
    public string FromCardNumber { get; init; }
    public string ToCardNumber { get; init; }
    public double Amount { get; init; }
}

