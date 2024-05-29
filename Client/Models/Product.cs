using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Specification { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]
        public DateTime ModifiedTime { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public int Stock { get; set; }

        public Category Category { get; set; }
        public List<InventoryLog> InventoryLogs { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public  List<CartItem> CartItems { get; set; }

        public Product()
        {
            InventoryLogs = new List<InventoryLog>();
            OrderItems = new List<OrderItem>();
            CartItems = new List<CartItem>();
        }
    }
}