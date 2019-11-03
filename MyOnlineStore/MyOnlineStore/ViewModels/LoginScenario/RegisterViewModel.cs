using MyOnlineStore.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.Extensions.Configuration;
using Xamarin.Forms.Xaml;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Views.LoginScenario;
using MyOnlineStore.Shared.Models.Users;
using MyOnlineStore.Interfaces.Factories;
using MyOnlineStore.ViewModels.Base;
using System.Collections.Generic;
using MyOnlineStore.Entries;
using MyOnlineStore.Interfaces.Validations;
using Xamarin.Essentials;

namespace MyOnlineStore.ViewModels.LoginScenario
{
    public class RegisterViewModel : BaseViewModel, IRegisterViewModel
    {
        public RegisterEntry RegisterModel { get; set; }
        public bool IsValid { get; set; }
        public Validations.ValidatableObject<string> Email { get; set; } 
        public Validations.ValidatableObject<string> Password { get; set; }
        public Validations.ValidatableObject<string> ConfirmPassword { get; set; } 
        public Validations.ValidatableObject<string> PhoneNumber { get; set; } 
        public Validations.ValidatableObject<DateTime> BirthDate { get; set; }
        public Validations.ValidatableObject<string> FullName { get; set; }

        public ICommand RegisterCommand { get; set; }
        public ICommand ToLoginCommand { get; set; }

        private readonly IUserDataStoreFactory _UserDataStoreFactory;
        private readonly IUserFactory _UserFactory;
        private readonly IValidatableObjectFactory _ValidatableObjectFactory;
       
        public RegisterViewModel(IUserDataStoreFactory userDataStoreFactory,IUserFactory userFactory,  IValidatableObjectFactory validatableObjectFactory, RegisterEntry register)
        {
            RegisterModel = register;
           
            _UserDataStoreFactory = userDataStoreFactory;
            _UserFactory = userFactory;
            _ValidatableObjectFactory = validatableObjectFactory;

            RegisterCommand = new Command(CreateNewUserInformation);
            ToLoginCommand = new Command(NavigateToLogin);

            InitializeValidations();
        }
     
        public void NavigateToCardRegistration()
        {
            var cardRegistrationPage = Startup.ServiceProvider.GetService<RegisterAccountPage>();
            Application.Current.MainPage = cardRegistrationPage;
        }

        public void CreateNewUserInformation()
        {
            IsBusy = true;

            AddConfirmPasswordValidation(Password);
            IsValid = ValidateEntries();

            if (IsValid)
            {
                // Copy to Entry Model
                RegisterModel = CopyDataToEntryModel(RegisterModel);

                // Check using email, if not exist create
                var clientUserDataStore = _UserDataStoreFactory.GetClientUserDataStore();

                ClientUser clientUser = clientUserDataStore.GetUserByType(RegisterModel.Email);

                if (clientUser == null)
                {
                    clientUser = _UserFactory.CreateSimpleClientUser(RegisterModel);
                    clientUserDataStore.AddItem(clientUser);
                    Task.Run(async ()=>await SetStorageCredentials(clientUser.Email, clientUser.PasswordHash));
                    NavigateToCardRegistration();
                }
                else
                {
                    // Exist, should go to login page
                    //  await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    // Pop Message Already have an account. 
                }
            }

            IsBusy = false;           
        }

        public void NavigateToLogin()
        {
            Application.Current.MainPage = Startup.ServiceProvider.GetService<LoginPage>();
        }
        private bool ValidateEntries()
        {
            bool isValidUser = Email.Validate();
            bool isValidFullName = FullName.Validate();
            bool isValidBirthDate = BirthDate.Validate();
            bool isValidPhoneNumber = PhoneNumber.Validate();
            bool isValidPassword = Password.Validate();
            bool isValidConfirmPassword = ConfirmPassword.Validate();

            return isValidUser && isValidFullName && isValidBirthDate && 
                isValidPhoneNumber && isValidPassword && isValidConfirmPassword; 
        }

        private void InitializeValidations()
        {
            Email = _ValidatableObjectFactory.CreateSimpleValidatebleObject("torres@gmail.com");
            FullName = _ValidatableObjectFactory.CreateSimpleValidatebleObject("Ricardo Torres");
            Password = _ValidatableObjectFactory.CreateSimpleValidatebleObject("Torres_0210");
            BirthDate = _ValidatableObjectFactory.CreateSimpleValidatebleObject(new DateTime(year: 1993, month: 02, day: 10));
            ConfirmPassword = _ValidatableObjectFactory.CreateSimpleValidatebleObject("Torres_0210");
            PhoneNumber = _ValidatableObjectFactory.CreateSimpleValidatebleObject("777-777-7777");

            AddValidations();
        }

        private void AddValidations()
        {
            Email.ValidationsRules.AddRange(
                new List<IValidationRule<string>>
                { new Validations.Rules.IsNotNullOrEmptyRule<string> { ValidationMessage = "A Email is required." },
                    new Validations.Rules.RegexValidationRule<string> { ValidationMessage = "Enter a proper Email text", RegexString = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" }
                });

            FullName.ValidationsRules.AddRange(
                 new List<IValidationRule<string>>
                 { new Validations.Rules.IsNotNullOrEmptyRule<string> { ValidationMessage = "A Name is required." },
                    new Validations.Rules.RegexValidationRule<string> { ValidationMessage = "Enter a proper Name text.", RegexString = @"^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+$" },
                 });

            BirthDate.ValidationsRules.Add(new Validations.Rules.BirthDateValidationRule { ValidationMessage = "Your are not that old." });

            PhoneNumber.ValidationsRules.AddRange(
                new List<IValidationRule<string>>
                 { new Validations.Rules.IsNotNullOrEmptyRule<string> { ValidationMessage = "A Phone Number is required." },
                    new Validations.Rules.RegexValidationRule<string> { ValidationMessage = "Enter a proper Phone Number text.", RegexString = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$" },
                 });


            Password.ValidationsRules.AddRange(new List<IValidationRule<string>>
                 { new Validations.Rules.IsNotNullOrEmptyRule<string> { ValidationMessage = "A Password is required." },
                    new Validations.Rules.RegexValidationRule<string> { ValidationMessage = "Password needs 1 Upper Case, Special Character, Number and length of 8-16", RegexString = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$_!%*?&])[A-Za-z\d@_$!%*?&]{8,16}$" },
                 });
           
        }

        private RegisterEntry CopyDataToEntryModel(RegisterEntry registerEntry)
        {
            registerEntry.Email = Email.Value;
            registerEntry.BirthDate = BirthDate.Value;
            registerEntry.FullName = FullName.Value;
            registerEntry.Password = Password.Value;
            return registerEntry;
        }

        private void AddConfirmPasswordValidation(Validations.ValidatableObject<string> passwordEntered)
        {
           
            ConfirmPassword.ValidationsRules.Clear();
            ConfirmPassword.ValidationsRules.AddRange(new List<Interfaces.Validations.IValidationRule<string>>
                 { new Validations.Rules.IsNotNullOrEmptyRule<string> { ValidationMessage = "A Password is required." },
                    new Validations.Rules.CompareValidationRule<string>{ ValidationMessage="The Passwords are not the same.", CompareToString = passwordEntered.Value}
                 });
        }
        
        private async Task SetStorageCredentials(string email,string pass)
        {
            try
            {
                //=======================================================================
                //
                // TODO: Encrypt data values (email,pass)
                //
                //=======================================================================
                await SecureStorage.SetAsync("auth_token_email", email);
                await SecureStorage.SetAsync("auth_token_pass", pass);

            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
        }
    }
}
