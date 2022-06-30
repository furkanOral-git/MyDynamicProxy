using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console
{
    public class Product
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public Product(string description)
        {
            Description = description;
        }
    }
}