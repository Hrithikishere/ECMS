using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class OrderOrderItemsDTO : OrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }

        public OrderOrderItemsDTO() {
        
            OrderItems = new List<OrderItemDTO>();
        }
    }
}
