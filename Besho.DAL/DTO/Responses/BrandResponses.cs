﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Besho.DAL.DTO.Responses
{
   public class BrandResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string MainImage { get; set; }

        public string MainImageUrl => $"https://localhost:7086/Images/{MainImage}";
    }

}
