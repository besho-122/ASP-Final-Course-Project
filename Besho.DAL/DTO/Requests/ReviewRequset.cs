using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.DAL.DTO.Requests
{
   public class ReviewRequset
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }    


       



    }
}
