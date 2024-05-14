using BankApp.Models.Account;
using BankApp.Models.Card;
using BankApp.Models.Client;

namespace BankTests;

using NUnit.Framework;
using Moq;
using System.Collections.Generic;

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

    // More tests can be added for other methods like WithdrawMoney, TransferMoney, etc.
}
