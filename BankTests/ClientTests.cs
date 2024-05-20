using BankApp.Models.Client;

namespace BankTests;

[TestFixture]
public class ClientTests
{
    private Client _client;

    [SetUp]
    public void SetUp()
    {
        // Arrange  
        _client = new Client("John", "Doe", 30);
    }

    [Test]
    public void Constructor_Initializes_FirstName()
    {
        // Act
        string firstName = _client.FirstName;

        // Assert
        Assert.That(firstName, Is.EqualTo("John"));
    }

    [Test]
    public void Constructor_Initializes_LastName()
    {
        // Act
        string lastName = _client.LastName;

        // Assert
        Assert.That(lastName, Is.EqualTo("Doe"));
    }

    [Test]
    public void Constructor_Initializes_Age()
    {
        // Act
        int age = _client.Age;

        // Assert
        Assert.That(age, Is.EqualTo(30));
    }

    [Test]
    public void Constructor_DefaultValues()
    {
        // Arrange
        var defaultClient = new Client();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(defaultClient.FirstName, Is.EqualTo(""));
            Assert.That(defaultClient.LastName, Is.EqualTo(""));
            Assert.That(defaultClient.Age, Is.EqualTo(0));
        });
    }
}
