using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class WindowButton
    {
        public Button button;
        public IntPtr window_handle;

        public WindowButton()
        {
            button = null;
            window_handle = IntPtr.Zero;
        }
    }
}
