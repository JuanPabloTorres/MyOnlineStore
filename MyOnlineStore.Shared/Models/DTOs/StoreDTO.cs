using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyOnlineStore.Shared.Models.Entries
{
    public class StoreDTO 
    {
        public StoreDTO()
        {

        }
        public StoreDTO(Store store)
        {
            StoreName = store.StoreName;
            StoreOwner = store.StoreOwnerName;
            Category = store.Category;
            Description = store.Description;
            IsFavorite = true;
            Logo = store.Logo;
        }
        public string StoreName { get; set; }
        public string StoreOwner { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
        public byte[] Logo { get; set; }

    }
}
