using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Stores
{
    [Table("ClientsFavoriteStores")]
    public class FavoriteStoreClient
    {
        public Guid UserId { get; set; }
        public ClientUser Client { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
