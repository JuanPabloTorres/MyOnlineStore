using MyOnlineStore.Shared.CustomAttribute;
using MyOnlineStore.Shared.Models.Interfaces;
using MyOnlineStore.Shared.Models.Stores;
using System;
using System.Collections.Generic;

namespace MyOnlineStore.Shared.Models.Users
{
    [ApiGenericEntity]
    public class ClientUser : BaseUser, IClientUser, ICardable
    {
        public ClientUser()
        {
            if (!CardsHasValue())
            {
                UserCards = new List<UserCard>();
            }
        }
        public ICollection<UserCard> UserCards { get; protected set; }
        public DateTime BirthDate { get; set; }
        public ICollection<FavoriteStoreClient> FavoriteStores { get; set; }

        public bool CardsHasValue()
        {
            return UserCards == null ? false : true;
        }
        public bool AddCard(UserCard userCard)
        {
            bool added = false;
            int count;
            try
            {
                if (!this.CardsHasValue())
                {
                    this.UserCards = new List<UserCard>();
                }

                count = this.UserCards.Count;

                this.UserCards.Add(userCard);

                if ((count + 1) == this.UserCards.Count)
                {
                    added = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AddCard User: {ex.Message}");
                added = false;
            }

            return added;
        }
    }
}
