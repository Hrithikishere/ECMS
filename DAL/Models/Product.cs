using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        [Key]
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
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }
        [Required]

        public DateTime ModifiedTime { get; set;}

        public virtual Category Category { get; set; }

        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public Product()
        {
            InventoryLogs = new List<InventoryLog>();
            OrderItems = new List<OrderItem>();
            CartItems = new List<CartItem>();
        }
    }
}
