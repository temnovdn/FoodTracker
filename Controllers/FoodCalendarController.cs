using FoodTracker.Data;
using FoodTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Controllers
{
    [Authorize]
    public class FoodCalendarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodCalendarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? date)
        {
            var day = date?.Date ?? DateTime.Today;
            var entries = _context.ConsumedFood
                .Where(c => c.Date.Date == day)
                .Select(c => new ConsumedFoodViewModel
                {
                    Id = c.Id,
                    Date = c.Date,
                    FoodId = c.FoodId,
                    FoodName = c.Food.Name,
                    QuantityGrams = c.QuantityGrams,
                    Food = c.Food
                })
                .ToList();

            var totalCalories = entries.Sum(e => e.Calories);
            var totalFats = entries.Sum(e => e.Fats);
            var totalCarbs = entries.Sum(e => e.Carbohydrates);
            var totalProteins = entries.Sum(e => e.Proteins);

            ViewBag.SelectedDate = day;
            ViewBag.TotalCalories = totalCalories;
            ViewBag.TotalFats = totalFats;
            ViewBag.TotalCarbohydrates = totalCarbs;
            ViewBag.TotalProteins = totalProteins;
            return View(entries);
        }

        public IActionResult Create(DateTime? date)
        {
            var vm = new ConsumedFoodViewModel
            {
                Date = date?.Date ?? DateTime.Today
            };
            FillFoodSelectList(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConsumedFoodViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.ConsumedFood.Add(new ConsumedFood
                {
                    Date = vm.Date.Date,
                    FoodId = vm.FoodId,
                    QuantityGrams = vm.QuantityGrams
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { date = vm.Date.ToString("yyyy-MM-dd") });
            }
            FillFoodSelectList(vm);
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var c = _context.ConsumedFood.Include(x => x.Food).FirstOrDefault(x => x.Id == id);
            if (c == null) return NotFound();
            var vm = new ConsumedFoodViewModel
            {
                Id = c.Id,
                Date = c.Date,
                FoodId = c.FoodId,
                FoodName = c.Food.Name,
                QuantityGrams = c.QuantityGrams,
                Food = c.Food
            };
            FillFoodSelectList(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ConsumedFoodViewModel vm)
        {
            if (id != vm.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var c = _context.ConsumedFood.Find(id);
                if (c == null) return NotFound();
                c.Date = vm.Date.Date;
                c.FoodId = vm.FoodId;
                c.QuantityGrams = vm.QuantityGrams;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { date = vm.Date.ToString("yyyy-MM-dd") });
            }
            FillFoodSelectList(vm);
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var c = _context.ConsumedFood.Include(x => x.Food).FirstOrDefault(x => x.Id == id);
            if (c == null)
                return NotFound();

            var vm = new ConsumedFoodViewModel
            {
                Id = c.Id,
                Date = c.Date,
                FoodId = c.FoodId,
                FoodName = c.Food.Name,
                QuantityGrams = c.QuantityGrams,
                Food = c.Food
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var c = _context.ConsumedFood.Find(id);
            if (c != null)
            {
                var date = c.Date;
                _context.ConsumedFood.Remove(c);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index), new { date = date.ToString("yyyy-MM-dd") });
            }
            return RedirectToAction(nameof(Index));
        }

        private void FillFoodSelectList(ConsumedFoodViewModel vm)
        {
            ViewBag.FoodId = new SelectList(_context.Food.OrderBy(f => f.Name).ToList(), "Id", "Name", vm.FoodId);
        }
    }
}
