using BankApp.Models.Client;

namespace BankTests;

using NUnit.Framework;

[TestFixture]
public class ClientTests
{
    private Client _client;

    [SetUp]
    public void Setup()
    {
        // Arrange: Create a new instance of the Client class
        _client = new Client("John", "Doe", 30);
    }

    [Test]
    public void Constructor_WithValidArguments_ShouldSetProperties()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        int age = 30;

        // Act: Already done in setup

        // Assert
        Assert.AreEqual(firstName, _client.FirstName);
        Assert.AreEqual(lastName, _client.LastName);
        Assert.AreEqual(age, _client.Age);
    }

    [Test]
    public void Constructor_WithDefaultValues_ShouldSetPropertiesToDefaults()
    {
        // Arrange
        string defaultFirstName = "";
        string defaultLastName = "";
        int defaultAge = 0;

        // Act: Already done in setup

        // Assert
        var defaultClient = new Client(); // Creating a new client with default constructor
        Assert.AreEqual(defaultFirstName, defaultClient.FirstName);
        Assert.AreEqual(defaultLastName, defaultClient.LastName);
        Assert.AreEqual(defaultAge, defaultClient.Age);
    }

    // Additional tests can be added for edge cases, validation, etc.
}
