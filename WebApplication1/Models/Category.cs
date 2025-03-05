using Microsoft.EntityFrameworkCore;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    [Precision(18, 2)]
    public decimal Price { get; set; }
}
