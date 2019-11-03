using System;

namespace MyOnlineStore.Interfaces.Services
{
    public interface ISoftwareKeyboardService
    {
        event EventHandler<SoftwareKeyboardEventArgs> KeyboardHeightChanged;
        bool IsKeyboardVisible { get; }
    }
    public class SoftwareKeyboardEventArgs : EventArgs
    {
        public SoftwareKeyboardEventArgs(int keyboardheight)
        {
            KeyboardHeight = keyboardheight;
        }

        public int KeyboardHeight { get; private set; }
    }
}
