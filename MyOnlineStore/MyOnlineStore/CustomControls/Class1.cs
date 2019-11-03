using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyOnlineStore.CustomControls
{
    public class Class1:Xamarin.Forms.CheckBox
    {
        public static readonly BindableProperty IsFavoriteProperty =
           BindableProperty.Create(nameof(IsFavorite), typeof(bool), typeof(FavoriteLottieAnimationView), false, BindingMode.TwoWay);

        private bool _isFavorite;

        public bool IsFavorite
        {
            get
            {
                _isFavorite = (bool)GetValue(IsFavoriteProperty);
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;

                SetValue(IsFavoriteProperty, value);
            }
        }
    }
}
