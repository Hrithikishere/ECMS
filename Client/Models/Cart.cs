using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public User User { get; set; }

        public List<CartItem> CartItems { get; set; }

        public Cart() 
        {
            CartItems = new List<CartItem>();
        }

    }
}
