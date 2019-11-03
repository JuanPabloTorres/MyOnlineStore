using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Utilities;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoresCollectionView : ContentView
    {
        public static readonly BindableProperty StoreListProperty = BindableProperty.Create(nameof(StoreList), typeof(IEnumerable<StoreDTO>),typeof(StoresCollectionView),null,BindingMode.TwoWay, propertyChanged: OnStoreListChanged);
        public static readonly BindableProperty ToggleFavoriteCommandProperty = BindableProperty.Create(nameof(ToggleFavoriteCommand), typeof(Command<CommandEventData>), typeof(StoresCollectionView), null, BindingMode.TwoWay);
        public static readonly BindableProperty SectionTitleProperty = BindableProperty.Create(nameof(SectionTitle), typeof(string), typeof(StoresCollectionView), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty NavigateToStoreDetailCommandProperty = BindableProperty.Create(nameof(ToggleFavoriteCommand), typeof(Command<CommandEventData>), typeof(StoresCollectionView), null, BindingMode.TwoWay);


        private IEnumerable<StoreDTO> _storelist;
        public IEnumerable<StoreDTO> StoreList
        {
            get
            {
                _storelist = (IEnumerable<StoreDTO>)GetValue(StoreListProperty);
                return _storelist;
            }
            set
            {
                _storelist = value;
                SetValue(StoreListProperty, value);
            }
        }

        private Command<CommandEventData> _toggleFavoriteCommand;
        public Command<CommandEventData> ToggleFavoriteCommand
        {
            get
            {
                _toggleFavoriteCommand = GetValue(ToggleFavoriteCommandProperty) as Command<CommandEventData>;
                return _toggleFavoriteCommand;
            }
            set
            {
                _toggleFavoriteCommand = value;
                SetValue(ToggleFavoriteCommandProperty, value);
            }
        }

        private Command<CommandEventData> _navigateToStoreDetailCommand;
        public Command<CommandEventData> NavigateToStoreDetailCommand
        {
            get
            {
                _navigateToStoreDetailCommand =
                    (Command<CommandEventData>)GetValue(NavigateToStoreDetailCommandProperty);
                return _navigateToStoreDetailCommand;
            }
            set
            {
                _navigateToStoreDetailCommand = value;
                SetValue(NavigateToStoreDetailCommandProperty, value);
            }
        }


        public string SectionTitle
        {
            get { return (string)GetValue(SectionTitleProperty); }
            set { SetValue(SectionTitleProperty, value); }
        }

        public StoresCollectionView()
        {
            InitializeComponent();
        }

        static void OnStoreListChanged(BindableObject bindable , object oldValue, object newValue)
        {
            var instance = (bindable as StoresCollectionView);
            var value = (IEnumerable<StoreDTO>)newValue;
            instance.SetValue(StoreListProperty, value);
            instance.StoreList = value; 
        }
    }
}