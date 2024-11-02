using CarShowRoom.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarShowRoom.Data
{
	public class AppDbContext:IdentityDbContext
	{
        public AppDbContext(DbContextOptions options):base(options) 
        {
                
        }

        public DbSet <Brand> Brands { get; set; }
        public DbSet <CarImage> CarImages { get; set; }
        public DbSet <CarModel> CarModels { get; set; }
        public DbSet <Menu> Menus { get; set; }
        public DbSet <News> News { get; set; }
        public DbSet <Slider> Sliders { get; set; }
        public DbSet <Feature> Features { get; set; }
        public DbSet <ContactUs>  ContactUs { get; set; }
        public DbSet<ContactSubmission> ContactSubmissions { get; set; }





    }
}

