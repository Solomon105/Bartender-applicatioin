using Microsoft.AspNetCore.Mvc;
using Bartender_applicatioin.Models;
using System.Collections.Generic;
using System.Linq;


public class CocktailMenuController : Controller
{
    private readonly AppDbContext _dbContext;

    public CocktailMenuController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        List<CocktailMenu> cocktailMenu = _dbContext.CocktailMenus.ToList();
        return View(cocktailMenu);
    }

    public IActionResult PlaceOrder(int id)
    {
        CocktailMenu cocktail = _dbContext.CocktailMenus.Find(id);
        if (cocktail == null)
        {
            return NotFound();
        }

        

        return RedirectToAction("Index");
    }

}
