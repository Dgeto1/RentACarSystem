using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string carBrand { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string CarModel { get; set; }

        [Required]
        public int CarYear { get; set; }

        [Required]
        public int CountPlaces { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string ShortInformation { get; set; }

        [Required]
        public double PriceDay { get; set; }
        public ICollection<UserCar> Users { get; set; }
    }
}
