using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RentACar.Models {
	public class ApplicationUser : IdentityUser {
		[Required]
		[StringLength(100, MinimumLength = 2)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(100, MinimumLength = 2)]
		public string LastName { get; set; }
		[Required]
		[StringLength(10, MinimumLength = 10)]
		public string PIN { get; set; }
		[Required]
		[StringLength(100)]
		public string Address { get; set; }
		public ICollection<UserCar> Cars { get; set; }
	}
}