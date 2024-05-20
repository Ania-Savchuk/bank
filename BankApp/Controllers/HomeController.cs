namespace BankApp.Controllers;
//клас HomeController поставлений в залежність від інтерфейсу ILogger<HomeController> logger
public class HomeController(ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        logger.LogInformation("Accessed index page");
        return View();
    }

    public IActionResult Privacy()
    {
        logger.LogInformation("Accessed privacy policy page");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            }
        );
    }
}