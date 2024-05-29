using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        public DateTime JoinDate { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string Role { get; set; }

        public List<Order> Orders { get; set; }
        public List<Cart> Carts { get; set; }

        public User() { 
        
            Orders = new List<Order>();
            Carts = new List<Cart>();
        }
    }
}
