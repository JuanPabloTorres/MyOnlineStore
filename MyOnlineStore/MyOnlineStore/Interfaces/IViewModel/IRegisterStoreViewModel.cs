using MyOnlineStore.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyOnlineStore.Interfaces.IViewModel
{
    public interface IRegisterStoreViewModel
    {
        bool SaveStore();
        ICommand NavigateToAddProductItemCommand { get; }
        ICommand GetLocationCommand { get; }
        ICommand StoreNameValidationCommand { get; }
        ICommand LongitudeValidationCommand { get; }
        ICommand LatitudeValidationCommand { get; }
        ICommand MoreInfoCommand { get; }
        IValidateableObject<string> StoreName { get; }
        IValidateableObject<string> CardNumber { get; }
        IValidateableObject<string> Longitude { get; }
        IValidateableObject<string> Latitude { get; }
        bool Validate();
        Task GetLocation();
        bool VerifiedCard { get; }
    }
}
