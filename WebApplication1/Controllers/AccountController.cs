using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using WebApplication1.Models;

public class AccountController : Controller
{
    private readonly MyDBContext _context;

    public AccountController(MyDBContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string name, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
        if (user == null)
        {
            ViewBag.Error = "⚠️ Sai tài khoản hoặc mật khẩu.";
            return View();
        }

        // Lưu thông tin đăng nhập vào Session
        HttpContext.Session.SetString("UserName", user.Name);
        HttpContext.Session.SetString("UserRole", user.Role);

        return RedirectToAction("Index", user.Role == "Admin" ? "Admin" : "Customer");
    }

    public IActionResult Logout()
    {
        // Xóa Session khi đăng xuất
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string name, string password, string confirmPassword)
    {
        // Kiểm tra tên tài khoản có bị trùng không
        if (_context.Users.Any(u => u.Name == name))
        {
            ViewBag.RegisterError = "⚠️ Tên đăng nhập đã tồn tại!";
            return View("Login"); // Quay lại trang đăng nhập & đăng ký
        }

        // Kiểm tra mật khẩu nhập lại có khớp không
        if (password != confirmPassword)
        {
            ViewBag.RegisterError = "⚠️ Mật khẩu không khớp!";
            return View("Login");
        }

        // Thêm người dùng mới
        var newUser = new User
        {
            Name = name,
            Password = password, // Không mã hóa (không an toàn, nên hash mật khẩu)
            Role = "Customer"
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "🎉 Đăng ký thành công! Vui lòng đăng nhập.";
        return RedirectToAction("Login");
    }
}
