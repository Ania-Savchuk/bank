using BankApp.Models.Account;

namespace BankTests;

using NUnit.Framework;

[TestFixture] //клас в якому знаходяться тести
public class AccountChoiceTests
{
    private AccountChoice _accountChoice;

    [SetUp] //ф-ія яка виконується перед кожним тестом 
    public void Setup()
    {
        _accountChoice = new AccountChoice();
    }
    
    [Test] // тест(ф-ія яка щось тестує)
    
    public void AccountId_WhenSetToValidValue_ShouldNotHaveValidationError()
    {
        // Arrange
        long accountId = 123456789;

        // Act
        _accountChoice.AccountId = accountId;

        // Assert
        Assert.That(_accountChoice.AccountId, Is.EqualTo(accountId));
    }

    [Test]
    public void AccountId_WhenSetToZero_ShouldNotHaveValidationError()
    {
        // Arrange
        long accountId = 0;

        // Act
        _accountChoice.AccountId = accountId;

        // Assert
        Assert.That(_accountChoice.AccountId, Is.EqualTo(accountId));
    }
}
