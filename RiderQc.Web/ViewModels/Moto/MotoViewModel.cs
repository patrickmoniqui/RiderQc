using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Moto
{
    public class MotoViewModel
    {
        public int MotoId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required, Range(0, int.MaxValue, ErrorMessage = "Please enter a valid year.")]
        public int? Year { get; set; }
        [Required]
        public int? Type { get; set; }
    }
}