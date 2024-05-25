namespace BankApp.Controllers;

//DI -> ILogger<AccountController> logger
// клас AccountController поставлений в залежність від інтерфейсу ILogger<AccountController> logger
// фреймворк вирішує ці залежності і передає відповідні об'єкти
// ILogger інтерфейс з бібліотеки фреймворку який використовується для логів(записи про дії програми)
public class AccountController(ILogger<AccountController> logger) : Controller
{
    private readonly ICardFactory _cardFactory = CardFactorySingleton.Instance; // Use the singleton factory instance

    // GET: AccountController
    [HttpGet]
    public ActionResult Index()
    {
        logger.LogInformation("Accessed account index page");
        return Redirect("Account/Register/");
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
        return RedirectToAction("AccountDetails", new { id = account.Id });
    }
    
    [HttpGet]
     public ActionResult AccountDetails(long id)
     {
         var account = AccountData.DatabaseStub.FirstOrDefault(a => a.Id == id);
         if (account == null)
         {
             logger.LogInformation("Account not found");
             return NotFound();
         }
    
         return View(account);
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

        Account? account = AccountData.DatabaseStub.FirstOrDefault(acc => acc.Id == model.AccountId);

        if (account is null)
        {
            return View(model);
        }

        AccountData.CurrentAccount = account;
        logger.LogInformation("Login account successfully");

        // Add redirect to user's page
        return RedirectToAction("AccountDetails", new { id = account.Id });
    }

    // Example method to add a card to an account using the factory

    [HttpGet]
    public ActionResult AddCard(long accountId)
    {
        ViewBag.AccountId = accountId;
        return View();
    }

    [HttpPost]
    public ActionResult AddCard(long accountId, string cardType)
    {
        var account = AccountData.DatabaseStub.FirstOrDefault(acc => acc.Id == accountId);

        if (account == null)
        {
            logger.LogInformation("Account not found");
            return NotFound();
        }

        ICard card;
        if (cardType == "credit")
        {
            card = _cardFactory.CreateCreditCard(account.Client, 10000, 5); // Example values
        }
        else
        {
            card = _cardFactory.CreateDebitCard(account.Client);
        }

        account.AddCard(card);
        logger.LogInformation($"{cardType} card added to account {accountId}");

        return RedirectToAction("CardDetails", new { accountId = accountId, cardNumber = card.CardNumber });
    }

    [HttpGet]
    public ActionResult CardDetails(long accountId, string cardNumber)
    {
        var account = AccountData.DatabaseStub.FirstOrDefault(acc => acc.Id == accountId);
        if (account == null)
        {
            logger.LogInformation("Account not found");
            return NotFound();
        }

        var card = account.GetCards().FirstOrDefault(c => c.CardNumber == cardNumber);
        if (card == null)
        {
            logger.LogInformation("Card not found");
            return NotFound();
        }
        ViewBag.AccountId = accountId;

        return View(card as Card);
    }
    
    [HttpGet]
    public ActionResult TransferFunds(long accountId)
    {
        var account = AccountData.DatabaseStub.FirstOrDefault(acc => acc.Id == accountId);
        if (account == null)
        {
            logger.LogInformation("Account not found");
            return NotFound();
        }

        var cards = account.GetCards().ToList(); // Перетворюємо на список для уникнення помилок
        if (cards == null || !cards.Any())
        {
            logger.LogInformation("No cards available for account");
            ModelState.AddModelError("", "No cards available for this account.");
            return View(new TransferViewModel { AccountId = accountId });
        }

        ViewBag.AccountId = accountId;
        ViewBag.Cards = cards;

        return View(new TransferViewModel { AccountId = accountId });
    }

    [HttpPost]
    public ActionResult TransferFunds(TransferViewModel model)
    {
        var account = AccountData.DatabaseStub.FirstOrDefault(acc => acc.Id == model.AccountId);
        if (account == null)
        {
            logger.LogInformation("Account not found");
            return NotFound();
        }

        var fromCard = account.GetCards().FirstOrDefault(c => c.CardNumber == model.FromCardNumber);
        var toCard = account.GetCards().FirstOrDefault(c => c.CardNumber == model.ToCardNumber);

        if (fromCard == null || toCard == null)
        {
            logger.LogInformation("Card not found");
            return NotFound();
        }

        Transfer transfer = new(fromCard, toCard, model.Amount);
        var status = transfer.ExecuteTransfer();

        switch (status)
        {
            case Transfer.TransferStatus.Success:
                logger.LogInformation("Transfer completed successfully");
                break;
            case Transfer.TransferStatus.NotEnough:
                ModelState.AddModelError("", "Not enough funds on the source card.");
                break;
            case Transfer.TransferStatus.TooMuch:
                ModelState.AddModelError("", "Too much amount for the destination card.");
                break;
            case Transfer.TransferStatus.WrongCardNumber:
                ModelState.AddModelError("", "Invalid card number.");
                break;
        }

        if (!ModelState.IsValid)
        {
            ViewBag.AccountId = model.AccountId;
            ViewBag.Cards = account.GetCards();
            return View(model);
        }

        return RedirectToAction("AccountDetails", new { id = model.AccountId });
    }
}



