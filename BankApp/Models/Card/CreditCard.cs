namespace BankApp.Models.Card;

public class CreditCard : Card
{
    private readonly double _interestRate;
    private double _monthlyContribution;

    public double CreditLimit { get; init; }
    public double MonthlyContribution => _monthlyContribution;
    public double InterestRate => _interestRate;
    

    public CreditCard(Client.Client owner, double limit, double rate) : base(owner, limit)
    {
        CreditLimit = limit;
        _interestRate = rate;
        CalculateMonthlyContribution();
    }

    public double GetInterestRate()
    {
        return _interestRate;
    }

    public double CalculateMonthlyContribution()
    {
        _monthlyContribution = CreditLimit - Balance * (_interestRate / 100.0 / 12.0);
        return _monthlyContribution;
    }

    public override bool Deposit(double amount)
    {
        // if (CreditLimit != Balance) // Loss of precision
        // if (Math.Abs(CreditLimit - Balance) > Double.Epsilon)

        if (Math.Abs(CreditLimit - Balance) < Double.Epsilon
            || amount <= 0)
            return false;

        Balance += amount;
        return true;
    }

    public override bool Withdraw(double amount)
    {
        if (amount < 0 || amount > CreditLimit - Balance)
        {
            return false;
        }

        Balance -= amount;
        return true;
    }

    public override string ToString()
    {
        StringBuilder bld = new();
        bld.Append("Card Type: Credit Card");
        bld.Append(base.ToString());
        bld.Append($"Credit Limit: {CreditLimit} USD");
        bld.Append($"Interest Rate: {GetInterestRate()}%");
        bld.Append($"Monthly Contribution: {CalculateMonthlyContribution()} USD");
        return bld.ToString();
    }
}