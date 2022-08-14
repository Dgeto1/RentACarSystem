namespace RentACar.Models
{
    public class UserCar
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public string UserName { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string RentDate { get; set; }
        public string ReturnDate { get; set; }
    }
}
