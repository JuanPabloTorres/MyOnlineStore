using MyOnlineStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreItemView : ContentView
    {
        public StoreItemView()
        {
            InitializeComponent();
            //BindingContext = new HomeViewModel();
        }
    }
}