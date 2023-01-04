using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers;

public class TagsController : Controller
{
  private readonly ToDoListContext _db;

  public TagsController(ToDoListContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    return View(_db.Tags.ToList());
  }

  public ActionResult Details(int id)
  {
    // get the List of all Tag objects from Tags in our database context
    Tag thisTag = _db.Tags
      // include the JoinEntities property of each Tag object
      .Include(tag => tag.JoinEntities)
      // then include the Item object of each ItemTag object stored in the JoinEntities property
      .ThenInclude(join => join.Item)
      // then match the id of the Tag object passed in through the id parameter to the TagId in out _dbTags database
      .FirstOrDefault(tag => tag.TagId == id);

    return View(thisTag);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Tag tag)
  {
    _db.Tags.Add(tag);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult AddItem(int id)
  {
    Tag thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
    ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");
    return View(thisTag);
  }

  [HttpPost]
  public ActionResult AddItem(Tag tag, int ItemId)
  {
    #nullable enable
    ItemTag? joinEntity = _db.ItemTags.FirstOrDefault(join => (join.ItemId == ItemId && join.TagId == tag.TagId));
    #nullable disable

    if (joinEntity == null && ItemId != 0)
    { 
      _db.ItemTags.Add(new ItemTag() { ItemId = ItemId, TagId = tag.TagId });
      _db.SaveChanges();
    }
    return RedirectToAction("Details", new { id = tag.TagId });
  }

  public ActionResult Edit(int id)
  {
    Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
    return View(thisTag);
  }

  [HttpPost]
  public ActionResult Edit(Tag tag)
  {
    _db.Tags.Update(tag);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Delete(int id)
  {
    Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
    return View(thisTag);
  }
  
  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
    _db.Tags.Remove(thisTag);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }

  [HttpPost]
  public ActionResult DeleteJoin(int joinId)
  {
    ItemTag joinEntry = _db.ItemTags.FirstOrDefault(entry => entry.ItemTagId == joinId);
    _db.ItemTags.Remove(joinEntry);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
}