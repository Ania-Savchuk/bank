namespace BankApp.Models.Client;

public class Client(
    string firstName = "", 
    string lastName = "", 
    int age = 0)
{
    public string FirstName { get; } = firstName;
    
    public string LastName { get; } = lastName;
    
    public int Age { get; } = age;
}

