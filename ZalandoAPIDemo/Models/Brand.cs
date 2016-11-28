using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoAPIDemo.Models
{
    public class Brand
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string LogoLargeUrl { get; set; }
        public BrandFamily BrandFamily { get; set; }
        public string ShopUrl { get; set; }
    }
}
