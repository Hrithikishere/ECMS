using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ProductInventoryLogDTO : ProductDTO
    {
        public List<InventoryLogDTO> InventoryLogs { get; set; }

        public ProductInventoryLogDTO()
        {
            InventoryLogs = new List<InventoryLogDTO>();
        }
    }
}
