using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    /// <summary>Consumed food as shown in UI: date, food name, quantity, nutrition per quantity.</summary>
    public class ConsumedFoodViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Food")]
        public int FoodId { get; set; }

        [Display(Name = "Food name")]
        public string FoodName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Quantity (grams)")]
        [Range(0.01, double.MaxValue)]
        public double QuantityGrams { get; set; }

        [Display(Name = "Calories")]
        public double Calories => Food == null ? 0 : Food.Calories * QuantityGrams / 100;

        [Display(Name = "Fats")]
        public double Fats => Food == null ? 0 : Food.Fats * QuantityGrams / 100;

        [Display(Name = "Carbohydrates")]
        public double Carbohydrates => Food == null ? 0 : Food.Carbohydrates * QuantityGrams / 100;

        [Display(Name = "Proteins")]
        public double Proteins => Food == null ? 0 : Food.Proteins * QuantityGrams / 100;

        public Food? Food { get; set; }
    }

    /// <summary>Daily totals for the calendar.</summary>
    public class DailyTotalsViewModel
    {
        public DateTime Date { get; set; }
        public double TotalCalories { get; set; }
        public double TotalFats { get; set; }
        public double TotalCarbohydrates { get; set; }
        public double TotalProteins { get; set; }
        public List<ConsumedFoodViewModel> Entries { get; set; } = new();
    }
}
