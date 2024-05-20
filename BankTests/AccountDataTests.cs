using BankApp.Models.Account;
namespace BankTests;
using NUnit.Framework;


[TestFixture]
public class AccountDataTests
{
    [Test]
    public void DatabaseStub_WhenInitialized_ShouldBeEmpty()
    {
        // Arrange & Act
        var databaseStub = AccountData.DatabaseStub;

        // Assert
        Assert.That(databaseStub, Is.Not.Null);
        Assert.That(databaseStub, Is.Empty);
    }

    [Test]
    public void CurrentAccount_WhenSet_ShouldBeRetrievable()
    {
        // Arrange
        var account = new Account();

        // Act
        AccountData.CurrentAccount = account;

        // Assert
        Assert.That(AccountData.CurrentAccount, Is.EqualTo(account));
    }

    [Test]
    public void CurrentAccount_WhenSetToNull_ShouldBeNullable()
    {
        // Arrange & Act
        AccountData.CurrentAccount = null;

        // Assert
        Assert.IsNull(AccountData.CurrentAccount);
    }

    [Test]
    public void CurrentAccount_WhenSetMultipleTimes_ShouldContainLastSetAccount()
    {
        // Arrange
        var account1 = new Account();
        var account2 = new Account();

        // Act
        AccountData.CurrentAccount = account1;
        AccountData.CurrentAccount = account2;

        // Assert
        Assert.That(AccountData.CurrentAccount, Is.EqualTo(account2));
    }

}
