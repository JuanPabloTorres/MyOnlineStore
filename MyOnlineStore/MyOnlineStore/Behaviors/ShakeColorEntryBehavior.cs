using MyOnlineStore.CustomControls;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;

namespace MyOnlineStore.Behaviors
{
    public class ShakeColorEntryBehavior : Behavior<ImageEntry>
    {
        public ImageEntry Entry { get; set; }
        public string RegexString { get; set; }
        public Color CorrectColor { get; set; }
        public Color WrongColor { get; set; }

        public static readonly BindableProperty ValidatableObjectProperty =
            BindableProperty.CreateAttached(nameof(ValidatableObject), typeof(Validations.ValidatableObject<string>), typeof(ShakeColorEntryBehavior),new Validations.ValidatableObject<string>());
        public Validations.ValidatableObject<string> ValidatableObject
        {
            get { return (Validations.ValidatableObject<string>)GetValue(ValidatableObjectProperty); }
            set { SetValue(ValidatableObjectProperty, value); }
        }
        enum TypeOfRegex
        {
            Password
        }
        public ShakeColorEntryBehavior()
        {
            
        }
        protected override void OnAttachedTo(ImageEntry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ReturnCommand = new Command(async()=> await ExecuteShake());
            bindable.Unfocused += Bindable_Unfocused;
            ValidatableObject.ValidationsRules.Add(new Validations.Rules.IsNotNullOrEmptyRule<string> { ValidationMessage = "A Email is required." });
        }
        protected override void OnDetachingFrom(ImageEntry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Unfocused -= Bindable_Unfocused;
        }
        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            if (!e.IsFocused)
            {
                Entry = sender as ImageEntry;

                if (Entry.ReturnCommand.CanExecute(null))
                {
                    Entry.ReturnCommand.Execute(null);

                    if (ValidatableObject != null)
                    {
                        ValidatableObject.IsValid = true;
                        ValidatableObject.Validate();
                        ValidatableObject.Errors = ValidatableObject.Errors;
                    }
                    
                }
            }
            
        }

        public async Task ExecuteShake()
        {
            bool regexPass;
            string text;

            if (Entry is ImageEntry entry && entry != null)
            {
                text = string.IsNullOrEmpty(Entry.Text) ? string.Empty : Entry.Text;
                regexPass = Regex.IsMatch(text, RegexString);

                if (regexPass)
                {
                    entry.TextColor = CorrectColor;
                }
                else
                {

                    entry.TextColor = WrongColor;
                    await entry.TranslateTo(-15, 0, 50);
                    await entry.TranslateTo(15, 0, 50);
                    await entry.TranslateTo(-10, 0, 50);
                    await entry.TranslateTo(10, 0, 50);
                    await entry.TranslateTo(-5, 0, 50);
                    await entry.TranslateTo(5, 0, 50);
                    entry.TranslationX = 0;
                }
            }
        }
        
    }
}
