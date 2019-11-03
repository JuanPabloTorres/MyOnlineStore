using MyOnlineStore.CustomControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyOnlineStore.Behaviors
{
    public class ShakeColorPasswordBehavior :Behavior<Entry>
    {
        public ImageEntry Entry { get; set; }
        public string RegexString = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$";
        public Color CorrectColor { get; set; }
        public Color WrongColor { get; set; }

        enum TypeOfRegex
        {
            Password
        }
        public ShakeColorPasswordBehavior()
        {

        }
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ReturnCommand = new Command(async () => await ExecuteShake());
            bindable.Unfocused += Bindable_Unfocused;
        }
        protected override void OnDetachingFrom(Entry bindable)
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
    }
}
