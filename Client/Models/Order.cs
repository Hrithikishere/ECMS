using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int TotalAmount { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        [Required]
        [StringLength(100)]

        public string ShippingAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string BillingAddress { get; set; }

        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }


    }
}
