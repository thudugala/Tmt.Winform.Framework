using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace TMTControls.TMTPanels
{
    public partial class TMTPanelHome : UserControl
    {
        public delegate void TileButtonClickedEventHandler(object sender, TileButtonClickedEventArgs e);

        [Category("TMT")]
        public event TileButtonClickedEventHandler TileButtonClicked;

        public TMTPanelHome()
        {
            InitializeComponent();
        }

        private void TMTPanelHome_Load(object sender, EventArgs e)
        {
            this.InitializeTileButtons();
        }

        public class TileButtonClickedEventArgs : EventArgs
        {
            public string PanelFullName { get; set; }
            public Assembly AssemblyOfType { get; set; }
        }

        public void InitializeTileButtons()
        {
            TMTTileButton tileButton;
            foreach (Control childControl in this.Controls)
            {
                if (childControl is TMTTileButton)
                {
                    tileButton = childControl as TMTTileButton;
                    if (string.IsNullOrWhiteSpace(tileButton.PanelName) == false)
                    {
                        tileButton.Click += tileButton_Click;
                    }
                }
            }
        }

        private void tileButton_Click(object sender, EventArgs e)
        {
            if (TileButtonClicked != null)
            {
                TMTTileButton myButton = sender as TMTTileButton;

                TileButtonClickedEventArgs arg = new TileButtonClickedEventArgs()
                {
                    AssemblyOfType = this.GetType().Assembly,
                    PanelFullName = string.Format("{0}.{1}", this.GetType().Namespace, myButton.PanelName)
                };
                TileButtonClicked(this, arg);
            }
        }
    }
}