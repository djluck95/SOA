namespace OrderClient.Models
{
    using System;    
    using System.Xml.Serialization;
    public class Order
    {

        public int id { get; set; }

        public string name { get; set; }

        public string quantity { get; set; }

        public string date { get; set; }
    }
}



