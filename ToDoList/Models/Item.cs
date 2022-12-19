using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    // Declare a static List<Item> _instances variable
    private static List<Item> _instances = new List<Item> {};
    public int Id { get; }

    public Item(string description)
    {
      Description = description;
      // Add created Item object to static _instances whenever Item is instantiated
      _instances.Add(this);
      Id = _instances.Count;
    }

    // Add a static Getter function to get _instances 
    public static List<Item> GetAll()
    {
      return _instances;
    }
    
    // Add function to clear data for fresh test cycling
    public static void ClearAll()
    {
      _instances.Clear();
    }

    public static Item Find(int searchId)
    {
      return _instances[searchId-1];
    }
  }
}