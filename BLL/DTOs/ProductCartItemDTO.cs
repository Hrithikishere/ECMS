using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ProductCartItemDTO : ProductDTO
    {
        public List<CartItemDTO> CartItems = new List<CartItemDTO>();

        public ProductCartItemDTO() { 
        
            CartItems = new List<CartItemDTO>();
        }
    }
}
