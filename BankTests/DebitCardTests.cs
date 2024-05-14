using BankApp.Models.Card;
using BankApp.Models.Client;

namespace BankTests;

[TestFixture]
public class DebitCardTests
{
    private Client _owner;
    private DebitCard _debitCard;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        _owner = new Client("Alice", "Smith");
        _debitCard = new DebitCard(_owner);
    }

    [Test]
    public void Deposit_ValidAmount_Increases_Balance()
    {
        // Arrange
        double initialBalance = _debitCard.Balance;
        double depositAmount = 500;

        // Act
        bool depositResult = _debitCard.Deposit(depositAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(depositResult);
            Assert.That(_debitCard.Balance, Is.EqualTo(initialBalance + depositAmount));
        });
    }

    [Test]
    public void Deposit_NegativeAmount_DoesNotChange_Balance()
    {
        // Arrange
        double initialBalance = _debitCard.Balance;
        double depositAmount = -500;

        // Act
        bool depositResult = _debitCard.Deposit(depositAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsFalse(depositResult);
            Assert.That(_debitCard.Balance, Is.EqualTo(initialBalance));
        });
    }

    [Test]
    public void Withdraw_NegativeAmount_DoesNotChange_Balance()
    {
        // Arrange
        double initialBalance = _debitCard.Balance;
        double withdrawAmount = -300;

        // Act
        bool withdrawResult = _debitCard.Withdraw(withdrawAmount);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.IsFalse(withdrawResult);
            Assert.That(_debitCard.Balance, Is.EqualTo(initialBalance));
        });
    }

    [Test]
    public void Withdraw_AmountExceedsBalance_ReturnsFalse()
    {
        // Arrange
        double initialBalance = _debitCard.Balance;
        double withdrawAmount = initialBalance + 100;

        // Act
        bool withdrawResult = _debitCard.Withdraw(withdrawAmount);

        // Assert
        Assert.IsFalse(withdrawResult);
    }
}