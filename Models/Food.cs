using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    /// <summary>Food item with nutrition facts per 100 grams.</summary>
    public class Food
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public required string Name { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Calories (per 100g)")]
        public double Calories { get; set; }

        [Required]
        [Display(Name = "Fats (per 100g)")]
        public double Fats { get; set; }

        [Required]
        [Display(Name = "Carbohydrates (per 100g)")]
        public double Carbohydrates { get; set; }

        [Required]
        [Display(Name = "Proteins (per 100g)")]
        public double Proteins { get; set; }
    }
}
