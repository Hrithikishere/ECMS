﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public int OrderId { get; set; }

        [Required]

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }

    }
}
