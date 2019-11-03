using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using MyOnlineStore.Views.Templates;

namespace MyOnlineStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public static BindableProperty StringProperty = BindableProperty.Create(nameof(String), typeof(string), typeof(Page1), null, BindingMode.TwoWay);
        public string String
        {
            get { return (string)GetValue(StringProperty); }
            set { SetValue(StringProperty, value); }
        }
        public Page1()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupView());
        }
    }
}