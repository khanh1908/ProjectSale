using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;

public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly MyDBContext _context;

    public CustomerController(ILogger<CustomerController> logger, MyDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index(string user)
    {
        ViewBag.User = user;
        var products = _context.Products.ToList();

        return View(products);
    }
}
