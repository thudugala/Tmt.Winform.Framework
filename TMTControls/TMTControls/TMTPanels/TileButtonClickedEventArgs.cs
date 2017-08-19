using System;
using System.Reflection;

namespace TMTControls.TMTPanels
{
    public class TileButtonClickedEventArgs : EventArgs
    {
        public string PanelFullName { get; set; }
        public Assembly AssemblyOfType { get; set; }
    }
}