using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Purchases
{
    [Table("Orders")]
    public class Order
    {
        public Guid OrderId { get; set; }
        public ICollection<ProductItem> OrderItems { get; set; }
        public Store StoreOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public string PurchaseBy { get; set; }
        public string PayedWith { get; set; }
        public bool IsCompleted { get; set; } = false;

    }
}
