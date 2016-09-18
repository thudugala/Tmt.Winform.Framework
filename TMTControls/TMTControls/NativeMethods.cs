using System;
using System.Runtime.InteropServices;

namespace TMTControls
{
    internal static class NativeMethods
    {
        // Used in KeyEntersEditMode function
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        internal static extern short VkKeyScan(char key);

        // Needed to forward keyboard messages to the child TextBox control.
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}