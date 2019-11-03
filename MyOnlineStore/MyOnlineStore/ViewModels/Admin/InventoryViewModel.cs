using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Interfaces.IViewModel;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels.Base;
using MyOnlineStore.Views.AdminScenarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.ViewModels.Admin
{
    public class InventoryViewModel : BaseViewModel, IInventoryViewModel
    {

        protected readonly IProductItemDataStore _ProductItemData;

        private ObservableCollection<ProductItem> _inventoryList;
        public ObservableCollection<ProductItem> InventoryItems
        {
            get => _inventoryList;
            set
            {
                _inventoryList = value;
                RaisePropertyChanged(() => InventoryItems);
            }
        }
        public ICommand NavigateToAddProductItemCommand { get; set; }

        public InventoryViewModel(IProductItemDataStore productItemDataStore)
        {
            _ProductItemData = productItemDataStore;

            NavigateToAddProductItemCommand = new Command(async () => await NavigateToAddProduct());
            MessagingCenter.Subscribe<InventoryPage>(this, "update", (sender) =>
            {
                InventoryItems = FetchData();
            });
        }

        private async Task NavigateToAddProduct()
        {
            await Shell.Current.GoToAsync("AddProductItem");
        }

        public ObservableCollection<ProductItem> FetchData()
        {
            _ProductItemData.GetAll();
            return new ObservableCollection<ProductItem>();
        }
    }
}
