using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrderStorage
{
    public class Stock
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string quantity { get; set; }

    }
}
