using MyOnlineStore.Shared.Models;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.Administrator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailItempage : ContentPage
    {
       // ItemDetailViewModel IdetailVm;
        public DetailItempage(ProductItem item)
        {
            InitializeComponent();

            //IdetailVm = new ItemDetailViewModel(item);
            //itemPic.Source = ImageSource.FromStream();
           // BindingContext = IdetailVm;
        }
    }
}