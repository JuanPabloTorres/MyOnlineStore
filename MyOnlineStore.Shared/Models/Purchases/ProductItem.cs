using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Purchases
{
    [Table("ProductItems")]
    public class ProductItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public byte[] Image { get; set; }

        public int Quantity { get; set; }

        public string Type { get; set; }

        public bool IsAvailable { get; set; }

        public string TextColor { get; set; }

        public int TotalPurchased { get; set; }

        

        [ForeignKey("MyStore")]
        public Guid MyStoreId { get; set; }
        public Store MyStore { get; set; }
    }
}
