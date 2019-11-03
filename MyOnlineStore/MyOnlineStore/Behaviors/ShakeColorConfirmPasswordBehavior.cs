using MyOnlineStore.CustomControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyOnlineStore.Behaviors
{
    class ShakeColorConfirmPasswordBehavior : Behavior<Entry>
    {
        public Entry Entry { get; set; }
        public string RegexString { get; set; } = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$";
        public Color CorrectColor { get; set; }
        public Color WrongColor { get; set; }

        
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ShakeColorConfirmPasswordBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty CompareToEntryProperty = BindableProperty.Create("CompareToEntry", typeof(Entry), typeof(ShakeColorConfirmPasswordBehavior), null);

        public Entry CompareToEntry
        {
            get
            {
                var obj = base.GetValue(CompareToEntryProperty);
                var entry = (Entry)obj;
                return entry;
            }
            set
            {
                base.SetValue(CompareToEntryProperty, value);
                if (CompareToEntry != null)
                    CompareToEntry.TextChanged -= baseValue_changed;
                value.TextChanged += baseValue_changed;
            }
        }
        void baseValue_changed(object sender, TextChangedEventArgs e)
        {
            IsValid = ((Entry)sender).Text.Equals(Entry.Text);
            Entry.TextColor = IsValid ? Color.Green : Color.Red;
        }
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            //bindable.TextChanged += HandleTextChanged;
            bindable.Unfocused += Bindable_Unfocused;
            bindable.ReturnCommand = new Command(async () => await ExecuteShake());
            base.OnAttachedTo(bindable);
        }
        public async Task ExecuteShake()
        {
            bool regexPass;
            string text;

            if (Entry is ImageEntry entry && entry != null)
            {
                text = string.IsNullOrEmpty(Entry.Text) ? string.Empty : Entry.Text;
                regexPass = Regex.IsMatch(text, RegexString);


                var tempentry = CompareToEntry ?? new Entry();

                var password = tempentry.Text;
                var confirmPassword = Entry.Text;

                IsValid = password.Equals(confirmPassword);

                if (regexPass && IsValid)
                {
                    entry.TextColor = CorrectColor;
                }
                else
                {

                    entry.TextColor = WrongColor;
                    await Entry.TranslateTo(-15, 0, 50);
                    await Entry.TranslateTo(15, 0, 50);
                    await Entry.TranslateTo(-10, 0, 50);
                    await Entry.TranslateTo(10, 0, 50);
                    await Entry.TranslateTo(-5, 0, 50);
                    await Entry.TranslateTo(5, 0, 50);
                    entry.TranslationX = 0;
                }

               
            }
        }
        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            if (!e.IsFocused)
            {
                Entry = sender as ImageEntry;

                if (Entry.ReturnCommand.CanExecute(null))
                {
                    Entry.ReturnCommand.Execute(null);
                }
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            //bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            
            var password = CompareToEntry.Text;
            var confirmPassword = e.NewTextValue;
            IsValid = password.Equals(confirmPassword);
        }
    }
}
