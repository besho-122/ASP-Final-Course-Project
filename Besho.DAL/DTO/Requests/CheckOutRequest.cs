using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.DTO.Requests
{
    public class CheckOutRequest
    {
        public PaymentMethodEnum PaymentMethod { get; set; } 
        

    }
}
