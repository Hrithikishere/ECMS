using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CartCartItemDTO : CartDTO
    {
        public List<CartItemDTO> CartItems { get; set; }

        public CartCartItemDTO() {

            CartItems = new List<CartItemDTO>();
        }
    }
}
