using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModels;

public class RegisterViewModel
{
  [Required]
  [EmailAddress]
  [Display(Name = "Email")]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "Password must be between 8 and 15 characters, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
  public string Password { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Confirm Password")]
  [Compare("Password", ErrorMessage = "Passwords do not match")]
  public string ConfirmPassword { get; set; }
}