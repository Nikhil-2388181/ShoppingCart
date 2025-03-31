using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;

namespace Shop.Data.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) 
        {
                
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(

                    new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "Fantasy", DisplayOrder = 3 },
                    new Category { Id = 4, Name = "Romance", DisplayOrder = 4 },
                    new Category { Id = 5, Name = "Comedy", DisplayOrder = 5 },
                    new Category { Id = 6, Name = "Horror", DisplayOrder = 6 },
                    new Category { Id = 7, Name = "Mystery", DisplayOrder = 7 },
                    new Category { Id = 8, Name = "Adventure", DisplayOrder = 8 },
                    new Category { Id = 9, Name = "Slice of Life", DisplayOrder = 9 },
                    new Category { Id = 10, Name = "Mecha", DisplayOrder = 10 }
                );
            modelBuilder.Entity<Product>().HasData(
          
                 new Product
                 {
                     Id = 1,
                     Title = "One Piece",
                     Author = "Eiichiro Oda",
                     Description = "Follow Monkey D. Luffy and his pirate crew in their quest to find the greatest treasure, the One Piece, and become the Pirate King.\r\n\r\nJoin them as they face formidable foes and uncover the mysteries of the Grand Line.",
                     ISBN = "SWD9999001",
                     ListPrice = 99,
                     Price = 90,
                     Price50 = 85,
                     Price100 = 80,
                     CategoryId = 3,
                     ImageUrl = ""


                 },
                new Product
                {
                    Id = 2,
                    Title = "Solo Leveling",
                    Author = "Chugong",
                    Description = "In a world where hunters with various magical powers battle deadly monsters, Sung Jin-Woo, the weakest of all hunters, finds himself in a never-ending struggle for survival.\r\n\r\nAs he discovers a mysterious power, he begins to level up beyond imagination.",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""



                },
                new Product
                {
                    Id = 3,
                    Title = "Dragon Ball Z",
                    Author = "Akira Toriyama",
                    Description = "The adventures of Goku and his friends as they defend the Earth from an assortment of villains ranging from intergalactic space fighters and conquerors, unnaturally powerful androids, and nearly indestructible creatures.\r\n\r\nWitness epic battles and transformations that push the limits of power.",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2,
                    ImageUrl = ""



                },
                new Product
                {
                    Id = 4,
                    Title = "Naruto",
                    Author = "Masashi Kishimoto",
                    Description = "Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the village's leader and strongest ninja.\r\n\r\nFollow his journey as he overcomes challenges and forms bonds with his friends.",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 7,
                    ImageUrl = ""


                },
                new Product
                {
                    Id = 5,
                    Title = "Black Clover",
                    Author = "Yūki Tabata",
                    Description = "Asta, a young boy born without any magic power, aims to become the Wizard King to prove his strength and protect his kingdom.\r\n\r\nWith determination and hard work, he faces numerous magical threats.",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 2,
                    ImageUrl=""
                    


                },
                new Product
                {
                    Id = 6,
                    Title = "Bleach",
                    Author = "Tite Kubo",
                    Description = "Ichigo Kurosaki, a teenager with the ability to see ghosts, gains the powers of a Soul Reaper and must protect the living world from evil spirits and guide the souls of the deceased to the afterlife.\r\n\r\nExperience his battles and growth as a protector of both worlds.",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 5,
                    ImageUrl = ""

                }

             );

                 
               
        }
    }
}
