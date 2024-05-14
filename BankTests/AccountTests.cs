using BankApp.Models.Account;
using BankApp.Models.Card;
using BankApp.Models.Client;
using BankApp.Models.TransferData;
using Moq;
using System.Collections.Generic;

namespace BankTests;


[TestFixture]
public class AccountTests
{
    private Account _account;
    private Client _client;
    private Mock<ICard> _mockCard;

    [SetUp]
    public void Setup()
    {
        _client = new Client("John", "Doe", 30);
        _account = new Account(_client, "1234567890");
        _mockCard = new Mock<ICard>();
    }

    [Test]
    public void Constructor_WithValidArguments_ShouldSetProperties()
    {
        // Arrange
        string expectedFirstName = "John";
        string expectedLastName = "Doe";
        int expectedAge = 30;
        string expectedPhoneNumber = "1234567890";

        // Act: Already done in setup

        // Assert
        Assert.AreEqual(expectedFirstName, _account.Client.FirstName);
        Assert.AreEqual(expectedLastName, _account.Client.LastName);
        Assert.AreEqual(expectedAge, _account.Client.Age);
        Assert.AreEqual(expectedPhoneNumber, _account.PhoneNumber);
    }

    [Test]
    public void AddCard_ShouldAddCardToAccount()
    {
        // Arrange
        _mockCard.Setup(card => card.Deposit(It.IsAny<double>())).Returns(true);
        _mockCard.Setup(card => card.Withdraw(It.IsAny<double>())).Returns(true);

        // Act
        _account.AddCard(_mockCard.Object);

        // Assert
        Assert.Contains(_mockCard.Object, ((List<ICard>)_account.GetCards()));
    }

    [Test]
    public void DepositMoney_WithValidCardNumber_ShouldDepositMoney()
    {
        // Arrange
        double amount = 100.0;
        _mockCard.Setup(card => card.Deposit(amount)).Returns(true);
        _account.AddCard(_mockCard.Object);

        // Act
        bool result = _account.DepositMoney(1, amount);

        // Assert
        Assert.IsTrue(result);
        _mockCard.Verify(card => card.Deposit(amount), Times.Once);
    }
    
    [Test]
    public void WithdrawMoney_WithValidCardNumberAndSufficientBalance_ShouldWithdrawMoney()
    {
        // Arrange
        double initialBalance = 200.0;
        double withdrawAmount = 100.0;
        _mockCard.Setup(card => card.Balance).Returns(initialBalance);
        _mockCard.Setup(card => card.Withdraw(withdrawAmount)).Returns(true);
        _account.AddCard(_mockCard.Object);

        // Act
        bool result = _account.WithdrawMoney(1, withdrawAmount);

        // Assert
        Assert.IsTrue(result);
        _mockCard.Verify(card => card.Withdraw(withdrawAmount), Times.Once);
    }

    [Test]
    public void WithdrawMoney_WithInvalidCardNumber_ShouldNotWithdrawMoney()
    {
        // Arrange
        _mockCard.Setup(card => card.Balance).Returns(0);
        _account.AddCard(_mockCard.Object);

        // Act
        bool result = _account.WithdrawMoney(2, 100.0); // Using an invalid card number

        // Assert
        Assert.IsFalse(result);
        _mockCard.Verify(card => card.Withdraw(It.IsAny<double>()), Times.Never);
    }

    [Test]
    public void TransferMoney_WithInvalidCardNumbers_ShouldReturnWrongCardNumberStatus()
    {
        // Arrange
        _account.AddCard(_mockCard.Object);
        var mockDestinationCard = new Mock<ICard>();
        _account.AddCard(mockDestinationCard.Object);

        // Act
        Transfer.TransferStatus result = _account.TransferMoney(0, 3, 100.0); // Using invalid card numbers

        // Assert
        Assert.AreEqual(Transfer.TransferStatus.WrongCardNumber, result);
        _mockCard.Verify(card => card.Withdraw(It.IsAny<double>()), Times.Never);
        mockDestinationCard.Verify(card => card.Deposit(It.IsAny<double>()), Times.Never);
}


    // More tests can be added for other methods like WithdrawMoney, TransferMoney, etc.
}
