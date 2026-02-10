using FoodTracker.Data;
using FoodTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTracker.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        private const int PageSize = 10;

        public IActionResult Index(int page = 1)
        {
            var query = _context.Food.OrderBy(f => f.Name);
            var totalCount = query.Count();
            var items = query.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
            ViewBag.TotalCount = totalCount;
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var food = _context.Food.Find(id);
            if (food == null)
                return NotFound();
            return View(food);
        }

        public IActionResult Create()
        {
            return View(new Food { Name = "" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Food.Add(food);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        public IActionResult Edit(int id)
        {
            var food = _context.Food.Find(id);
            if (food == null)
                return NotFound();
            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Food food)
        {
            if (id != food.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(food);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        public IActionResult Delete(int id)
        {
            var food = _context.Food.Find(id);
            if (food == null)
                return NotFound();
            return View(food);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var food = _context.Food.Find(id);
            if (food != null)
            {
                _context.Food.Remove(food);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
