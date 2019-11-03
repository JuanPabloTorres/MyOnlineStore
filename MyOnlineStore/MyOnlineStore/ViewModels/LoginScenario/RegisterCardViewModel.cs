using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.Entries;
using MyOnlineStore.Testing.MockData.Entries;
using System;
using System.Collections.Generic;
using MyOnlineStore.Interfaces.Validations;
using MyOnlineStore.Validations.Rules;

namespace MyOnlineStore.ViewModels.LoginScenario
{
    public class RegisterCardViewModel : IRegisterCardViewModel
    {
        //public readonly MockRegisterEntry Register;
        public readonly RegisterEntry Register;
        public UserCardEntry UserCardEntry { get; set; }

        public Validations.ValidatableObject<string> CardNumber { get; set; }
        public Validations.ValidatableObject<string> TypeOfCard { get; set; }
        public Validations.ValidatableObject<string> Pin { get; set; }
        public Validations.ValidatableObject<DateTime>  ExpirationDate  { get; set; }

        public ICommand CompleteCommand { get; set; }
        private readonly IUserCardDataStore _UserCardDataStore;
        private readonly IUserDataStoreFactory _UserDataStoreFactory;
       

        public RegisterCardViewModel(IUserDataStoreFactory userDataStoreFactory, IUserCardDataStore userCardDataStore, RegisterEntry register, UserCardEntry userCardEntry)
        {
            _UserDataStoreFactory = userDataStoreFactory;
            _UserCardDataStore = userCardDataStore;

            UserCardEntry = userCardEntry;
            Register = register;

            CompleteCommand = new Command( () => RegisterCompleted());

            ValidationsInit();
        }

        public void RegisterCompleted()
        {
            bool addedCard, updateUser;
            int cardCount;
            IUserDataStore<ClientUser> userDataStore;
            ClientUser currentUser;

            if (UserCardEntry != null)
            {
                bool isValidEntry = Validate();

                if (isValidEntry)
                {
                    //Add card to Register.
                    Register.CardInformation = UserCardEntry;
                    userDataStore = _UserDataStoreFactory.GetClientUserDataStore();
                    currentUser = userDataStore.GetUserByType(Register.Email);

                    if (currentUser != null)
                    {
                        cardCount = currentUser.UserCards.Count;
                        currentUser.UserCards.Add(Register.CardInformation.ToUserCard());
                        updateUser = _UserCardDataStore.AddItem(Register.CardInformation.ToUserCard());

                        addedCard = currentUser.UserCards.Count > cardCount ? true : false;

                        if (addedCard && updateUser)
                        {
                            // Log to app
                            Application.Current.MainPage = Startup.ServiceProvider.GetService<AppShell>();
                        }
                    }
                    else
                    {
                        //User doesnt exist, go to register.
                    }
                }
            }
        }

        private void ValidationsInit()
        {
            CardNumber = new Validations.ValidatableObject<string>();
            TypeOfCard = new Validations.ValidatableObject<string>();
            Pin = new Validations.ValidatableObject<string>();
            ExpirationDate = new Validations.ValidatableObject<DateTime>();

            AddValidationRules();
        }

        private void AddValidationRules()
        {
            //----------------------------------------------------------
            // TODO: Add Regex for every entry
            //CardNumber.Validations.AddRange(
            //    new List<Interfaces.Validations.IValidationRule<string>>
            //    { new IsNotNullOrEmptyRule<string> { ValidationMessage = "A Card Number is required." },
            //        new RegexValidationRule<string> { ValidationMessage = "Enter correct format for Card Number", RegexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" }
            //    });
        }

       private bool Validate()
        {
            bool isValidCardNumber = CardNumber.Validate();
            bool isValidTypeOfCard = TypeOfCard.Validate();
            bool isValidPin = Pin.Validate();
            bool isValidExpirationDate = ExpirationDate.Validate();

            return isValidCardNumber && isValidExpirationDate && isValidPin && isValidTypeOfCard;
        }
    }
}
