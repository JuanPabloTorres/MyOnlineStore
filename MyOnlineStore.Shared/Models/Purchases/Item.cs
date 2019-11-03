using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Purchases
{
   public  class Item
    {
        public string title { get; set; }

        public string description { get; set; }

        public List<string> images { get; set; }
    }
}
