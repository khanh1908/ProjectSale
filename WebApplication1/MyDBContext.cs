using Microsoft.EntityFrameworkCore;

public class MyDBContext : DbContext
{
    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
