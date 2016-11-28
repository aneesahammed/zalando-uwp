using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoAPIDemo.Models
{
    public class Unit
    {
        public string Id { get; set; }
        public string Size { get; set; }
        public Price Price { get; set; }
        public OriginalPrice OriginalPrice { get; set; }
        public bool Available { get; set; }
        public int Stock { get; set; }
        public string PartnerId { get; set; }
    }
}
