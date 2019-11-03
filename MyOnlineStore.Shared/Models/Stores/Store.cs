using MyOnlineStore.Shared.Models.Purchases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Stores
{
    [Table("Stores")]
    public class Store
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; }
        public string StoreOwnerName { get; set; }
        public string  Category { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public Location Location { get; set; }
        public List<ProductItem> MyProducts { get; set; }

        //public static bool EqualId(Store storeA,Store storeB)
        //{
        //    bool equalid = false;
        //    if (!(storeA is null && storeB is null))
        //    {
        //        equalid = storeA.Id == storeB.Id;
        //    }
        //    return equalid;
        //}
    }
}
