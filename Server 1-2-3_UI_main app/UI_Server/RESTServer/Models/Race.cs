using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Race
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int DistanceInMeters { get; set; }
        [Required]
        public long TimeInSeconds { get; set; }
    }
}
