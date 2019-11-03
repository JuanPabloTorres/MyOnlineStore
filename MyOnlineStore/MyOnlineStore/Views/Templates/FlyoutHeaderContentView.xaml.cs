using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeaderContentView : ContentView
    {
        public static BindableProperty HeaderContentViewProperty = BindableProperty.Create(nameof(HeaderContentView),typeof(View),typeof(FlyoutHeaderContentView),null,propertyChanged: OnHeaderContentChanged);
        public View HeaderContentView
        {
            get { return (View)GetValue(HeaderContentViewProperty); }
            set { SetValue(HeaderContentViewProperty, value); }
        }

        public FlyoutHeaderContentView()
        {
            InitializeComponent();
        }
        static void OnHeaderContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as FlyoutHeaderContentView;
            var value = newValue as StackLayout;
            instance.SetValue(HeaderContentViewProperty, value);
            instance.HeaderContentView = value;
        }
    }
}