using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Shared.Models.Interfaces
{
    public interface ICardable
    {
        ICollection<UserCard> UserCards { get; }
        bool CardsHasValue();
        bool AddCard(UserCard userCard);
    }
}
