using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Interfaces
{
    public interface IBaseEntity
    {
         string CreatedBy { get; set; }
         string UpdatedBy { get; set; }
         DateTime CreatedDateTime { get; set; }
         DateTime UpdateDateTime { get; set; }
    }
}
