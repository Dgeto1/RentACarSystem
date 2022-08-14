using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.Models;

namespace RentACar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public new DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Car> Cars { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCar>(entity =>
            {
                entity.HasKey(userCar => new { userCar.UserId, userCar.CarId });

                entity.HasOne(UserCar => UserCar.User)
                .WithMany(user => user.Cars)                                                                                                
                .HasForeignKey(userCar => userCar.UserId);

                entity.HasOne(userCar => userCar.Car)
                    .WithMany(car => car.Users)
                    .HasForeignKey(userCar => userCar.CarId);

            });
            
        }
        public DbSet<RentACar.Models.UserCar> UserCar { get; set; }
        
    }

}
