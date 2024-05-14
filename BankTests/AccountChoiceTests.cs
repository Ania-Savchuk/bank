using BankApp.Models.Account;

namespace BankTests;

using NUnit.Framework;

[TestFixture]
public class AccountChoiceTests
{
    private AccountChoice _accountChoice;

    [SetUp]
    public void Setup()
    {
        _accountChoice = new AccountChoice();
    }

    [Test]
    public void AccountId_WhenSetToValidValue_ShouldNotHaveValidationError()
    {
        // Arrange
        long accountId = 123456789;

        // Act
        _accountChoice.AccountId = accountId;

        // Assert
        Assert.AreEqual(accountId, _accountChoice.AccountId);
    }

    [Test]
    public void AccountId_WhenSetToZero_ShouldNotHaveValidationError()
    {
        // Arrange
        long accountId = 0;

        // Act
        _accountChoice.AccountId = accountId;

        // Assert
        Assert.AreEqual(accountId, _accountChoice.AccountId);
    }
    // Add more tests as needed
}
