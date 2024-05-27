using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class InventoryLog
    {
        public int Id { get; set; }
        [Required]

        public int ProductId { get; set; }
        [Required]
        public int ChangeAmount { get; set; }
        [Required]
        public string ReasonType { get; set; }
        [Required]
        public string ReasonDescription { get; set; }
        public Product Product { get; set; }

    }
}
