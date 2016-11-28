using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoAPIDemo.Models
{
    public class Image
    {
        public int OrderNumber { get; set; }
        public string Type { get; set; }
        public string ThumbnailHdUrl { get; set; }
        public string SmallUrl { get; set; }
        public string SmallHdUrl { get; set; }
        public string MediumUrl { get; set; }
        public string MediumHdUrl { get; set; }
        public string LargeUrl { get; set; }
        public string LargeHdUrl { get; set; }
    }
}
