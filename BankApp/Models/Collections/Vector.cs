namespace BankApp.Models.Collections;

public class Vector<T>
{
    private T[] _data;
    private int _size;
    private int _capacity;

    private void Resize(int newCapacity)
    {
        var newData = new T[newCapacity];
        for (int i = 0; i < _size; ++i)
        {
            newData[i] = _data[i];
        }
        _data = newData;
        _capacity = newCapacity;
    }

    public Vector()
    {
        _size = 0;
        _capacity = 4;
        _data = new T[_capacity];
    }

    public void PushBack(T value)
    {
        if (_size == _capacity)
        {
            Resize(2 * _capacity);
        }
        _data[_size++] = value;
    }

    public int Size
    {
        get { return _size; }
    }

    public T this[int index]
    {
        get { return _data[index]; }
        set { _data[index] = value; }
    }
}