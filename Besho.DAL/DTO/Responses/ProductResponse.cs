﻿using Besho.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Besho.DAL.DTO.Responses
{
   public  class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public string MainImage { get; set; }

        public string MainImageUrl { get; set; }    
        public List<string> SubImagesUrls { get; set; } = new List<string>();   

    }
}
