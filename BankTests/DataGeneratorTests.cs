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