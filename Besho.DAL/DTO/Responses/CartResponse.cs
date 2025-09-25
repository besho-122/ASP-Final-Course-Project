using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.DTO.Responses
{
    public class CartResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public int Count {get; set; }   
        public decimal Price {get; set; }

        public decimal TotalPrice => Price * Count; 


    }
}
