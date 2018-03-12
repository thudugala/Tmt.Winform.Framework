using System;
using System.Reflection;

namespace TMT.Controls.WinForms.Panels
{
    public class TileButtonClickedEventArgs : EventArgs
    {
        public Type NavigatePanel { get; set; }
        public Assembly AssemblyOfType { get; set; }
    }
}