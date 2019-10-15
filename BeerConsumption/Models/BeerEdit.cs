using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerConsumption.Models
{
    public class BeerEdit
    {
        public string Title { get; set; }
        public bool NonAlcohol { get; set; }
        public decimal Volume { get; set; }
        public int DrankQuantity { get; set; }
    }
}
