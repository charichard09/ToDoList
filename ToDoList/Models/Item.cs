using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class Item
  {
    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; }
    public int ItemId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "You must add your item to a category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<ItemTag> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}