using AlbelliChallenge.Business.Models;
using System.Collections.Generic;

namespace AlbelliChallenge.Data.Models
{
    public class Order 
    {
        public int OrderId { get; set; }
        public IEnumerable<ProductDetails> Products { get; set; }
    }

    public class OrderDetails 
    {
        public IEnumerable<ProductDetails> Products { get; set; }
        public double RequiredBinWidth { get; set; }
    }
}