using System.Collections.Generic;

namespace ZalandoAPIDemo.Models
{
    public class RootObject
    {
        public List<Content> Content { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
