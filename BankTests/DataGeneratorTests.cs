using BankApp.Models.Generators;

namespace BankTests;

[TestFixture]
public class DataGeneratorTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void GenerateCardNumber_ReturnsValidCardNumber()
    {
        // Arrange

        // Act
        string cardNumber = DataGenerator.GenerateCardNumber();

        // Assert
        Assert.IsNotNull(cardNumber);
        Assert.That(cardNumber.Length, Is.EqualTo(19)); // 16 digits + 3 spaces
        Assert.IsTrue(cardNumber.Replace(" ", "").All(char.IsDigit)); // All characters except spaces should be digits
    }

    [Test]
    public void GenerateIban_ReturnsValidIban()
    {
        // Arrange

        // Act
        string iban = DataGenerator.GenerateIban();

        // Assert
        Assert.IsNotNull(iban);
        Assert.That(iban.Length, Is.EqualTo(29)); // "UA" + 27 digits
        Assert.IsTrue(iban.StartsWith("UA")); // IBAN should start with "UA"
        Assert.IsTrue(iban.Substring(2).All(char.IsDigit)); // All characters after "UA" should be digits
    }

    [Test]
    public void GeneratePassword_ReturnsValidPassword()
    {
        // Arrange

        // Act
        string password = DataGenerator.GeneratePassword();

        // Assert
        Assert.IsNotNull(password);
        Assert.That(password.Length, Is.EqualTo(4)); // Password length should be 4
        Assert.IsTrue(password.All(char.IsDigit)); // All characters should be digits
    }


    [Test]
    public void TwoCardNumberShouldNotBeEqual()
    {
        // Arrange
        
        // Act
        string cn1 = DataGenerator.GenerateCardNumber(),
            cn2 = DataGenerator.GenerateCardNumber();
        
        // Assert
        Assert.That(cn1, Is.Not.EqualTo(cn2));
    }
    
    [Test]
    public void TwoIbansShouldNotBeEqual()
    {
        // Arrange
        
        // Act
        string iban1 = DataGenerator.GenerateIban(),
            iban2 = DataGenerator.GenerateIban();
        
        // Assert
        Assert.That(iban1, Is.Not.EqualTo(iban2));
    }
    
    [Test]
    public void TwoPasswordsShouldNotBeEqual()
    {
        // Arrange
        
        // Act
        string pwd1 = DataGenerator.GeneratePassword(),
            pwd2 = DataGenerator.GeneratePassword();
        
        // Assert
        Assert.That(pwd1, Is.Not.EqualTo(pwd2));
    }
}