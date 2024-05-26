using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InventoryLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int ChangeAmount { get; set; }
        [Required]
        public string ReasonType { get; set; }
        [Required]
        public string ReasonDescription { get; set; }
        public virtual Product Product { get; set; }

    }
}
