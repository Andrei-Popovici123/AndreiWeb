using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AndreiWeb.Models;

public class ShoppingCart
{
    [Key] public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [ValidateNever]
    public Product Product { get; set; }

    [Range(1, 1000, ErrorMessage = "Cant purchase more than 1000 at a time")]
    public int Count { get; set; }

    public string ApplicationUserId { get; set; }

    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }
    
    [NotMapped] public double Price { get; set; }
}