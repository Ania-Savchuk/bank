using BankApp.Models.Card;
using BankApp.Models.Client;

namespace BankTests;

[TestFixture]
public class CardTests
{
    private Client _owner;
    private double _initialBalance;
    private Card _card;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        _owner = new Client("John", "Doe");
        _initialBalance = 1000;
        _card = new DebitCard(_owner);
        _card.Deposit(_initialBalance);
    }

    [Test]
    public void Constructor_Initializes_CardNumber()
    {
        // Act
        string cardNumber = _card.CardNumber;

        // Assert
        Assert.That(cardNumber, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Constructor_Initializes_Password()
    {
        // Act
        string password = _card.Password;

        // Assert
        Assert.That(password, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Constructor_Initializes_IBAN()
    {
        // Act
        string iban = _card.Iban;

        // Assert
        Assert.That(iban, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Constructor_Sets_Balance()
    {
        // Act
        double balance = _card.Balance;

        // Assert
        Assert.That(balance, Is.EqualTo(_initialBalance));
    }

    [Test]
    public void Deposit_PositiveAmount_Increases_Balance()
    {
        // Arrange
        double depositAmount = 500;

        // Act
        bool depositResult = _card.Deposit(depositAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(depositResult);
            Assert.That(_card.Balance, Is.EqualTo(_initialBalance + depositAmount));
        });
    }

    [Test]
    public void Deposit_NegativeAmount_DoesNotChange_Balance()
    {
        // Arrange
        double depositAmount = -500;

        // Act
        bool depositResult = _card.Deposit(depositAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsFalse(depositResult);
            Assert.That(_card.Balance, Is.EqualTo(_initialBalance));
        });
    }

    [Test]
    public void Withdraw_ValidAmount_Decreases_Balance()
    {
        // Arrange
        double withdrawAmount = 500;

        // Act
        bool withdrawResult = _card.Withdraw(withdrawAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(withdrawResult);
            Assert.That(_card.Balance, Is.EqualTo(_initialBalance - withdrawAmount));
        });
    }

    [Test]
    public void Withdraw_InvalidAmount_DoesNotChange_Balance()
    {
        // Arrange
        double withdrawAmount = -500;

        // Act
        bool withdrawResult = _card.Withdraw(withdrawAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsFalse(withdrawResult);
            Assert.That(_card.Balance, Is.EqualTo(_initialBalance));
        });
    }

    [Test]
    public void Withdraw_AmountExceedsBalance_ReturnsFalse()
    {
        // Arrange
        double withdrawAmount = _initialBalance + 100;

        // Act
        bool withdrawResult = _card.Withdraw(withdrawAmount);

        // Assert
        Assert.IsFalse(withdrawResult);
    }

    [Test]

    public void Withdraw_NegativeAmount_ShouldReturnFalseAndNotChangeBalance()
    {
        // Arrange
        double initialBalance = _card.Balance;

        // Act
        bool isWithdrawn = _card.Withdraw(-50.0);

        // Assert
        Assert.IsFalse(isWithdrawn);
        Assert.AreEqual(initialBalance, _card.Balance);
    }
}