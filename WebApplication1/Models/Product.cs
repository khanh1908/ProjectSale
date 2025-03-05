using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Product
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    [Precision(18, 2)]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
