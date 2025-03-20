using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly MyDBContext _context;

    public AdminController(MyDBContext context)
    {
        _context = context;
    }
    public IActionResult Index(string user)
    {
        ViewBag.User = user;
        var products = _context.Products.ToList();

        return View(products);
    }
}
