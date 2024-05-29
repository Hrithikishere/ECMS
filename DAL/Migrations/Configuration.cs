namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ECMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.ECMSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            
            //seed category
            var categories = new List<Category>
            {
                new Category { Name = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Name = "Clothing", Description = "Apparel and fashion accessories" },
                new Category { Name = "Toys and Games", Description = "Toys and Games accessories" },
                new Category { Name = "Groceries", Description = "Everyday Groceries accessories" },
            };

            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(c => c.Name, category);
            }

            //seed customer
            var customers = new List<User>
            {
                new User { FirstName = "Md. Abdur", LastName = "Rouf", Email = "rouf@example.com", Password = "password0", Address = "Kuril, Dhaka, Bangladesh", JoinDate = DateTime.Now, Phone = "000-000-0000", Role = "Admin" },
                new User { FirstName = "Md. Abdur", LastName = "Rahim", Email = "rahim@example.com", Password = "password1", Address = "Uttara, Dhaka, Bangladesh", JoinDate = DateTime.Now, Phone = "123-456-7890", Role = "Customer" },
                new User { FirstName = "Md. Abdur", LastName = "Karim", Email = "karim@example.com", Password = "password2", Address = "Badda, Dhaka, Bangladesh", JoinDate = DateTime.Now, Phone = "987-654-3210", Role = "Customer" },

            };

            foreach (var customer in customers)
            {
                context.Users.AddOrUpdate(c => c.Email, customer);
            }

            //seed products
            var products = new List<Product>
            {
                // Electronics
                new Product { Name = "Smartphone", Specification = "Model XYZ", Description = "High-end smartphone", Price = 599.99, CategoryId = 1, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\5700g.jpg", Stock=10},
                new Product { Name = "Laptop", Specification = "Model ABC", Description = "Powerful laptop for productivity", Price = 999.99, CategoryId = 1, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\a7mark3.jpg", Stock=15 },
                new Product { Name = "Wireless Earbuds", Specification = "Model PQR", Description = "Bluetooth earbuds for music lovers", Price = 149.99, CategoryId = 1, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\ad80hw.jpg", Stock=15 },

                // Clothing
                new Product { Name = "Jeans", Specification = "Size 32", Description = "Classic denim jeans", Price = 39.99, CategoryId = 2, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\shirt.jpg", Stock=15 },
                new Product { Name = "T-Shirt", Specification = "Size M", Description = "Casual cotton t-shirt", Price = 19.99, CategoryId = 2, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\tshirt.jpg", Stock=15 },
                new Product { Name = "Dress Shirt", Specification = "Size L", Description = "Formal dress shirt for men", Price = 29.99, CategoryId = 2, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\panjabi.jpg", Stock=15 },

                // Toys and Games
                new Product { Name = "Board Game", Specification = "Ages 8+", Description = "Classic family board game", Price = 29.99, CategoryId = 3, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\chess.jpg", Stock=15 },
                new Product { Name = "Action Figure", Specification = "Superhero", Description = "Collectible action figure", Price = 14.99, CategoryId = 3, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\playpen.jpg", Stock=15 },
                new Product { Name = "LEGO Set", Specification = "Space theme", Description = "Building blocks for creative play", Price = 49.99, CategoryId = 3, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\board.jpg", Stock=15 },

                // Groceries
                new Product { Name = "Milk", Specification = "1 gallon", Description = "Fresh dairy milk", Price = 2.99, CategoryId = 4, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now , ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\alubukhara.jpg", Stock=15},
                new Product { Name = "Bread", Specification = "Whole wheat", Description = "Healthy whole wheat bread", Price = 3.49, CategoryId = 4, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now , ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\redchilli.jpg", Stock=15},
                new Product { Name = "Eggs", Specification = "Dozen", Description = "Farm-fresh eggs", Price = 1.99, CategoryId = 4, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="C:\\Users\\rrouf\\Documents\\Dot Net\\Job Project\\ECMS\\Client\\Assets\\Images\\masala.jpg", Stock=15 }
            };

            foreach (var product in products)
            {
                context.Products.AddOrUpdate(p => p.Name, product);
            }
            
        }
    }
}
