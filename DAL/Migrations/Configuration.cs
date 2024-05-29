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
                new User { FirstName = "Md. Abdur", LastName = "Rouf", Email = "rouf@user.com", Password = "password0", Address = "Kuril, Dhaka, Bangladesh", JoinDate = DateTime.Now, Phone = "000-000-0000", Role = "Admin" },
                new User { FirstName = "Dr. Sabid", LastName = "Dehan", Email = "dehan@user.com", Password = "password1", Address = "Uttara, Dhaka, Bangladesh", JoinDate = DateTime.Now, Phone = "123-456-7890", Role = "Customer" },
                new User { FirstName = "Md. Twaaha", LastName = "Hossain", Email = "twaaha@user.com", Password = "password2", Address = "Badda, Dhaka, Bangladesh", JoinDate = DateTime.Now, Phone = "987-654-3210", Role = "Customer" },

            };

            foreach (var customer in customers)
            {
                context.Users.AddOrUpdate(c => c.Email, customer);
            }

            //seed products
            var products = new List<Product>
            {
                // Electronics
                new Product { Name = "AMD Ryzen 7 5700G", Specification = "Model: Ryzen 7 5700G, Speed: 3.8GHz up to 4.6GHz, Cache: L2: 4MB, L3: 16MB, Cores-8 & Threads-16, Memory Speed: Up to 3200MHz", Description = "The AMD Ryzen 7 5700G Processor with Radeon Graphics is built in intelligence featuring 8 processor cores, 16 threads, and an astonishingly efficient 45-65W TDP. In this processor, It stands with 3.8GHz Base Clock, 4.6GHz Max Boost Clock, 4MB L2 Cache, 16MB L3 Cache with AM4 Package and TSMC 7nm FinFET CMOS. This processor is built with PCIe 3.0 x8 and Wraith Spire Thermal Solution. The AMD Ryzen 7 5700G Processor provides DDR4 Up to 3200MHz memory with 2 memory channels. With the high processing capability, this processor is also integrated with Radeon Graphics. The processor has a graphics frequency of 2000MHz and a graphics Core count of 8.", Price = 188.00, CategoryId = 1, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\5700g.jpg", Stock=50},
                new Product { Name = "Sony Alpha A7 III", Specification = "5-axis image stabilization BIONZ X image processing engine, 35 mm 24.3 MP 7 Exmor CMOS sensor, High-resolution OLED Tru-Finder", Description = "Capture the peaks of more decisive moments with the Î±7 III from Sony, packing newly developed back-illuminated full-frame CMOS sensor and other advanced imaging innovations, high-speed response, ease of operation, and reliable durability that are ready for various shooting needs. A software upgrade is now available to support Sony's new Real-time Eye AF for Animals. The feature works by automatically detecting and tracking the eyes of animals that you're photographing, for beautiful pet portraits and wildlife shots.", Price = 1509.99, CategoryId = 1, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\a7mark3.jpg", Stock=55 },
                new Product { Name = "Huawei AD80HW", Specification = "Display diagonal: 23.8 \", Display resolution:1920 x 1080 pixels, Native aspect ratio: 16:9, Panel type:IPS, Display brightness (typical)\t250 cd/m², Response time: 5 ms", Description = "the Huawei AD80HW is a 23.8-inch monitor with Full HD resolution, an IPS panel, and a flat screen shape. It offers a brightness level of 250 cd/m², a response time of 5 ms, and a contrast ratio of 1000:1. While it provides a satisfactory viewing experience, no further information is available about its specific features and connectivity options.", Price = 149.99, CategoryId = 1, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\ad80hw.jpg", Stock=42 },

                // Clothing
                new Product { Name = "Shirt", Specification = "Size 32, Color: Maroon", Description = "Good quality cotton t-shirt for men shirt", Price = 39.99, CategoryId = 2, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\shirt.jpg", Stock=65 },
                new Product { Name = "T-Shirt", Specification = "Size M, Color: Ash", Description = "Good quality cotton t-shirt for men", Price = 19.99, CategoryId = 2, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\tshirt.jpg", Stock=25 },
                new Product { Name = "Semi Fitted Panjabi", Specification = "SKU: DMLP16929, Color: Blue", Description = "Good quality panjabi for men", Price = 29.99, CategoryId = 2, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\panjabi.jpg", Stock=15 },

                // Toys and Games
                new Product { Name = "Magnetic Chess Board", Specification = "Ages 8+", Description = "Classic chess board game", Price = 29.99, CategoryId = 3, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\chess.jpg", Stock=11 },
                new Product { Name = "Play Pen", Specification = "For Kids", Description = "New Bord Baby Playground", Price = 14.99, CategoryId = 3, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\playpen.jpg", Stock=16 },
                new Product { Name = "Bingo Magic Board", Specification = "1+ Year", Description = "Educational Toys which boost up child brain and Ergonomically designed without sharp edge", Price = 49.99, CategoryId = 3, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\board.jpg", Stock=19 },

                // Groceries
                new Product { Name = "Alu Bukhara", Specification = "100 gm", Description = "Alu Bukhara, also known as plums, is a healthy food item. When dried, they are known as prunes. These sweet and juicy fruits are rich in vitamins K, C, and A, and contain antioxidants.", Price = 2.99, CategoryId = 4, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now , ImagePath="Assets\\Images\\alubukhara.jpg", Stock=35},
                new Product { Name = "Dried Chillies (Shukna Morich)", Specification = "100 gm", Description = "Origin: Bangladesh", Price = 3.49, CategoryId = 4, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now , ImagePath="Assets\\Images\\redchilli.jpg", Stock=15},
                new Product { Name = "Fresh Biryani Masala", Specification = "Biryani Masala", Description = "Origin: Bangladesh", Price = 1.99, CategoryId = 4, CreatedTime = DateTime.Now, ModifiedTime = DateTime.Now, ImagePath="Assets\\Images\\masala.jpg", Stock=59 }
            };

            foreach (var product in products)
            {
                context.Products.AddOrUpdate(p => p.Name, product);
            }
            
        }
    }
}
