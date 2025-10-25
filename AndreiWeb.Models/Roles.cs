using System.ComponentModel.DataAnnotations;

namespace AndreiWeb.Models;

public class Roles
{
    [Key] public int Id { get; set; }
    [Required] public RoleName Name { get; set; }
    
}
public enum RoleName{
 Admin,
 Customer,
 User
}