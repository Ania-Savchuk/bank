using BankApp.Models.Card;
using BankApp.Models.TransferData;

namespace BankTests;

using Moq;

[TestFixture]
public class TransferTests
{
    private Mock<ICard> _mockFromCard;
    private Mock<ICard> _mockToCard;
    private Transfer _transfer;

    [SetUp]
    public void Setup()
    {
        _mockFromCard = new Mock<ICard>();
        _mockToCard = new Mock<ICard>();
        _transfer = new Transfer(_mockFromCard.Object, _mockToCard.Object, 100.0);
    }

    [Test]
    public void ExecuteTransfer_WhenFromCardHasEnoughBalance_ShouldReturnSuccess()
    {
        // Arrange
        _mockFromCard.Setup(card => card.Withdraw(It.IsAny<double>())).Returns(true);
        _mockToCard.Setup(card => card.Deposit(It.IsAny<double>())).Returns(true);

        // Act
        var result = _transfer.ExecuteTransfer();

        // Assert
        Assert.AreEqual(Transfer.TransferStatus.Success, result);
    }

    [Test]
    public void ExecuteTransfer_WhenFromCardDoesNotHaveEnoughBalance_ShouldReturnNotEnough()
    {
        // Arrange
        _mockFromCard.Setup(card => card.Withdraw(It.IsAny<double>())).Returns(false);

        // Act
        var result = _transfer.ExecuteTransfer();

        // Assert
        Assert.AreEqual(Transfer.TransferStatus.NotEnough, result);
    }

    [Test]
    public void ExecuteTransfer_WhenToCardCannotDepositAmount_ShouldReturnTooMuch()
    {
        // Arrange
        _mockFromCard.Setup(card => card.Withdraw(It.IsAny<double>())).Returns(true);
        _mockToCard.Setup(card => card.Deposit(It.IsAny<double>())).Returns(false);

        // Act
        var result = _transfer.ExecuteTransfer();

        // Assert
        Assert.AreEqual(Transfer.TransferStatus.TooMuch, result);
    }

}
