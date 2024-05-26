using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ProductOrderItemDTO : ProductDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }

        public ProductOrderItemDTO() { 
        
            OrderItems = new List<OrderItemDTO>();
        }    
    }
}
