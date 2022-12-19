using System;
using System.Collections.Generic;
using ToDoList.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList
{
  public class Program
  {
    public static void Main(string[] args)
    {
      #region Create Web App
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllersWithViews();

      WebApplication app = builder.Build();

      app.UseHttpsRedirection();

      app.UseStaticFiles();

      app.UseRouting();

      app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
      );

      app.Run();
      #endregion
      
      #region ToDoList business logic Week8
      Console.WriteLine("To Do List");
      Console.WriteLine("--------------");
      bool finished = false;
      while (!finished)
      {
        Console.WriteLine("Would you like to add an item, view your list, or exit? (Add/View/Exit)");
        string userResponse = Console.ReadLine().ToUpper(); 
        if (userResponse == "ADD")
        {
          //take user input
          //process and return to start of loop
          Console.WriteLine("Enter the description for the new item.");
          string description = Console.ReadLine();
          Item newItem = new Item(description);
          Console.WriteLine($"{description} has been added to your list.");
          finished = IsFinished();
        }
        else if (userResponse == "VIEW")
        {
          // T newList =              // { Item }
          List<Item> newList = Item.GetAll();
          int counter = 1;
          if (newList.Count == 0)
          {
            Console.WriteLine("You don't have anything to do, loser. Get a life");
            finished = IsFinished();
          }
        else
          {
            foreach (Item toDoItem in newList)
            {
              // 1045: to access description, added .Description property to toDoItem iteration
              Console.WriteLine($"{counter}. {toDoItem.Description}");
              counter++;
            }
            finished = IsFinished();
          }
        }
      }
      #endregion
    }

    #region IsFinished
    private static bool IsFinished()
    {
      Console.WriteLine("Would you like to add another item or view your list (y/n)");
      string userSecondResponse = Console.ReadLine().ToUpper();
      if (userSecondResponse == "Y") 
      {
        return false;
      } 
      else 
      {
        return true;
      }
    }
    #endregion
  }
}