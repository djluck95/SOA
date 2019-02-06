using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrderStorage
{
    public class Order
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string quantity { get; set; }

        [Required]
        public string date { get; set; }

    }
}
