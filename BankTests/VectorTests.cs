using System.Text;
using BankApp.Models.Collections;

namespace BankTests;

[TestFixture] // test class
public class VectorTests
{
    private Vector<int> _vector;

    [SetUp]
    public void Setup()
    {
        // Arrange: Create a new instance of Vector<int>
        _vector = new Vector<int>();
    }

    [Test]
    public void PushBack_WhenAddingElement_SizeIncreases()
    {
        // Arrange

        // Act
        _vector.PushBack(5);

        // Assert
        Assert.AreEqual(1, _vector.Size);
    }

    [Test]
    public void PushBack_MultipleTimes_SizeIncreasesCorrectly()
    {
        // Arrange

        // Act
        _vector.PushBack(5);
        _vector.PushBack(10);
        _vector.PushBack(15);

        // Assert
        Assert.AreEqual(3, _vector.Size);
    }

    [Test]
    public void PushBack_WhenCapacityExceeded_CapacityIncreases()
    {
        // Arrange
        int initialCapacity = _vector.Size;

        // Act
        for (int i = 0; i < 5; i++)
        {
            _vector.PushBack(i);
        }

        // Assert
        Assert.Greater(_vector.Size, initialCapacity);
    }

    [Test]
    public void Indexer_GetElement_ReturnsCorrectValue()
    {
        // Arrange
        _vector.PushBack(5);
        _vector.PushBack(10);
        _vector.PushBack(15);

        // Act
        int valueAtIndex1 = _vector[1];

        // Assert
        Assert.AreEqual(10, valueAtIndex1);
    }

    [Test]
    public void Indexer_SetElement_UpdatesCorrectly()
    {
        // Arrange
        _vector.PushBack(5);
        _vector.PushBack(10);
        _vector.PushBack(15);

        // Act
        _vector[1] = 20;

        // Assert
        Assert.AreEqual(20, _vector[1]);
    }

    [Test]
    public void Indexer_GetNegativeIndex_ThrowsIndexOutOfRangeException()
    {
        // Arrange
        _vector.PushBack(5);

        // Act & Assert
        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            var value = _vector[-1];
        });
    }
}