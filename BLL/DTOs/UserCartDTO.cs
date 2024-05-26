using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UserCartDTO : UserDTO
    {
        public List<CartDTO> Carts;

        public UserCartDTO() { 
        
            Carts = new List<CartDTO>();

        }
    }
}
