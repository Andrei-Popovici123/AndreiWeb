using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AndreiWeb.Models;

public class Product
{
    [Key] public int Id { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }
    [Required] public string Key { get; set; }

    [Required] public string Seller { get; set; }

    [Required]
    [Display(Name = " List Price")]
    [Range(1, 1000)]
    public double ListPrice { get; set; }

    [Required]
    [Display(Name = " List Price")]
    [Range(1, 1000)]
    public double Price { get; set; }

    [Required]
    [Display(Name = " List Price")]
    [Range(1, 1000)]
    public double Price50 { get; set; }

    [Required]
    [Display(Name = " List Price")]
    [Range(1, 1000)]
    public double Price100 { get; set; }
}