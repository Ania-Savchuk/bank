namespace BankApp.Models.TransferData;

// primary constructor
// creates private fields that are requested
public class Transfer(ICard fromCard, ICard toCard, double amount)
{
    public enum TransferStatus
    {
        Indeterminate,
        Success,
        NotEnough,
        TooMuch,
        WrongCardNumber
    }
    
    public TransferStatus ExecuteTransfer()
    {
        bool canWithdraw = fromCard.Withdraw(amount);
        if (!canWithdraw) return TransferStatus.NotEnough;

        bool canDeposit = toCard.Deposit(amount);
        if (!canDeposit) return TransferStatus.TooMuch;

        return TransferStatus.Success;
    }
}