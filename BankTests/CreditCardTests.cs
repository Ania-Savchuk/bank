using BankApp.Models.Card;
using BankApp.Models.Client;

namespace BankTests;

[TestFixture]
public class CreditCardTests
{
    private Client _owner;
    private double _creditLimit;
    private double _interestRate;
    private CreditCard _creditCard;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        _owner = new Client("Jane", "Doe");
        _creditLimit = 5000;
        _interestRate = 12;
        _creditCard = new CreditCard(_owner, _creditLimit, _interestRate);
    }

    [Test]
    public void Constructor_Initializes_CreditLimit()
    {
        // Act
        double creditLimit = _creditCard.CreditLimit;

        // Assert
        Assert.That(creditLimit, Is.EqualTo(_creditLimit));
    }

    [Test]
    public void Constructor_Initializes_InterestRate()
    {
        // Act
        double interestRate = _creditCard.GetInterestRate();

        // Assert
        Assert.That(interestRate, Is.EqualTo(_interestRate));
    }

    [Test]
    public void Constructor_Calculates_InitialMonthlyContribution()
    {
        // Arrange
        double expectedMonthlyContribution = _creditLimit;

        // Act
        double monthlyContribution = _creditCard.CalculateMonthlyContribution();

        // Assert
        Assert.That(monthlyContribution, Is.EqualTo(expectedMonthlyContribution));
    }

    [Test]
    public void Deposit_ValidAmount_Increases_Balance()
    {
        // Arrange
        double depositAmount = 1000;

        // Act
        bool depositResult = _creditCard.Deposit(depositAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(depositResult);
            Assert.That(_creditCard.Balance, Is.EqualTo(depositAmount));
        });
    }

    [Test]
    public void Deposit_NegativeAmount_DoesNotChange_Balance()
    {
        // Arrange
        double depositAmount = -500;

        // Act
        bool depositResult = _creditCard.Deposit(depositAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsFalse(depositResult);
            Assert.That(_creditCard.Balance, Is.EqualTo(0));
        });
    }

    [Test]
    public void Withdraw_ValidAmount_Decreases_Balance()
    {
        // Arrange
        double withdrawAmount = 1000;

        // Act
        bool withdrawResult = _creditCard.Withdraw(withdrawAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(withdrawResult);
            Assert.That(_creditCard.Balance, Is.EqualTo(-withdrawAmount));
        });
    }

    [Test]
    public void Withdraw_NegativeAmount_DoesNotChange_Balance()
    {
        // Arrange
        double withdrawAmount = -500;

        // Act
        bool withdrawResult = _creditCard.Withdraw(withdrawAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsFalse(withdrawResult);
            Assert.That(_creditCard.Balance, Is.EqualTo(0));
        });
    }

    [Test]
    public void Withdraw_AmountExceedsCreditLimit_ReturnsFalse()
    {
        // Arrange
        double withdrawAmount = _creditLimit + 100;

        // Act
        bool withdrawResult = _creditCard.Withdraw(withdrawAmount);

        // Assert
        Assert.IsFalse(withdrawResult);
    }
}