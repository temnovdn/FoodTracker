namespace FoodTracker.Models
{
    /// <summary>Record of food consumed on a date.</summary>
    public class ConsumedFood
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FoodId { get; set; }
        public double QuantityGrams { get; set; }

        public Food Food { get; set; } = null!;
    }
}
