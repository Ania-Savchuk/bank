using System.ComponentModel.DataAnnotations;

namespace BankApp.Models.Account;

public class AccountViewModel
{
    // перевірка на наявність
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = String.Empty;

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; }= String.Empty;

    [Required(ErrorMessage = "Age is required")]
    // перевірка чи вік більше за 18
    [Range(18, int.MaxValue, ErrorMessage = "Age must be 18 or older")]
    public int Age { get; set; }

    // перевірка чи стрічка є номером телефону (+ (\+) і 12 цифр (\d{12})
    [RegularExpression(@"^\+\d{12}$", ErrorMessage = "Phone number must be 10 digits")]
    public string PhoneNumber { get; set; }= String.Empty;
}