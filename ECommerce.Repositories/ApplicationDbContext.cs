using ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Luxury",
                    Description = "Luxury Items"
                },
                new Category
                {
                    Id = 2,
                    Name = "One day",
                    Description = "One Day Usage"
                },
                new Category
                {
                    Id = 3,
                    Name = "Electronics",
                    Description = "Electronics items"
                }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name ="Sofa",
                    Description ="Costly chairs",
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 2,
                    Name = "Spoon papers",
                    Description = "take foods",
                    CategoryId = 2,
                },
                new Product
                {
                    Id = 3,
                    Name = "Television",
                    Description = "for entertainment",
                    CategoryId = 3,
                }
              );

            base.OnModelCreating(builder);
        }
    }

}