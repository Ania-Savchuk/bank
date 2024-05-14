using BankApp.Models.Client;

namespace BankApp.Controllers;

//DI -> ILogger<AccountController> logger
public class AccountController(ILogger<AccountController> logger) : Controller
{
    // GET: AccountController
    [HttpGet] 
    public ActionResult Index()
    {
        logger.LogInformation("Accessed account index page");
        return Redirect($"Account/Register/");
    }

    // GET: Account/Register
    [HttpGet]
    public ActionResult Register()
    {
        logger.LogInformation("Accessed register page");
        return View();
    }

    // POST: Account/Register
    [HttpPost]  
    public ActionResult Register(AccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            logger.LogInformation("Failed to create account");
            return View(model);
        }

        // Logic to save account (kind of)
        Account account = new(
            new Client(model.FirstName, model.LastName, model.Age),
            model.PhoneNumber
        );
        AccountData.DatabaseStub.Add(account);
        logger.LogInformation("Created account successfully");

        // Redirect to home page after successful account creation
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public ActionResult Login()
    {
        logger.LogInformation("Trying to log in");
        return View();
    }

    [HttpPost]
    public ActionResult Login(AccountChoice model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Account? account = AccountData.DatabaseStub
            .FirstOrDefault(
                acc => acc.Id == model.AccountId
            );

        if (account is null)
        {
            return View(model);
        }

        AccountData.CurrentAccount = account;
        
        // Add redirect to user's page
        return Redirect($"Home/Index/");
    }
}