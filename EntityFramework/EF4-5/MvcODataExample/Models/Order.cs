using System.Collections.Generic;

namespace MvcODataExample.Models {
    public class Order {
        public string OrderNo { get; set; }
        public string SerialNo { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}