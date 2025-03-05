using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class CartController : Controller
{
    private readonly MyDBContext _context;

    public CartController(MyDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cartItems = await _context.CartItems.Include(c => c.Product).ToListAsync();
        return View(cartItems);
    }

    public async Task<IActionResult> AddToCart(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == productId);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = 1
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int id, int quantity)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem != null && quantity > 0)
        {
            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }

    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
