using System.Text;
using BankApp.Models.Collections;

namespace BankTests;

[TestFixture] // test class
public class VectorTests
{
    private Vector<string> _v;
    
    [SetUp] // executes before every test
    public void Setup()
    {
        _v = new Vector<string>();
    }

    [Test]
    public void ShouldNotBeEmptyAfterElementAdded()
    {
        // Arrange
        string str = "abc";
        
        // Act
        _v.PushBack(str);
        
        // Assert
        int size = _v.Size;
        Assert.That(size, Is.Not.EqualTo(0));
    }
}