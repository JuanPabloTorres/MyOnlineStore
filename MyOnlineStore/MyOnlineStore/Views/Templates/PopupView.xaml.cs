using System;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace MyOnlineStore.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView 
    {
        public PopupView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}