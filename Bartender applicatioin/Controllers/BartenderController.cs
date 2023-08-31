using Bartender_applicatioin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bartender_applicatioin.Controllers
{
    public class BartenderController : Controller 
    {
        private readonly AppDbContext _dbContext;

        public BartenderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OrderQueue()
        {
            var orderQueue = _dbContext.CocktailOrders.ToList();
            return View(orderQueue);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string newStatus)
        {
            var order = _dbContext.CocktailOrders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            // Ensure the new status is valid (e.g., "Completed" or "Cancelled")
            if (newStatus == "Completed" || newStatus == "Cancelled")
            {
                order.Status = newStatus;
                _dbContext.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Invalid status value. Please select 'Completed' or 'Cancelled'.");
                var orderQueue = _dbContext.CocktailOrders.ToList();
                return View("OrderQueue", orderQueue);
            }

            return RedirectToAction("OrderQueue");
        }

        [HttpPost]
        public IActionResult PlaceOrder(int cocktailId)
        {
            // Find the selected cocktail by its ID
            var selectedCocktail = _dbContext.CocktailMenus.Find(cocktailId);

            if (selectedCocktail == null)
            {
                return NotFound(); // Handle case where selected cocktail doesn't exist
            }

            // Create a new CocktailOrder object
            var newOrder = new CocktailOrder
            {
                CocktailName = selectedCocktail.Name,
                CustomerName = "John Doe",
                Status = "Pending" 
            };

            // Add the new order to the database and save changes
            _dbContext.CocktailOrders.Add(newOrder);
            _dbContext.SaveChanges();

            // Optionally, redirect to a confirmation page or show a confirmation message
            TempData["Message"] = "Your order has been placed!";
            return RedirectToAction("PlaceOrder");
        }


    }
}
