using System;
using System.ComponentModel;
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

        public class TileButtonClickedEventArgs : EventArgs
        {
            public string PanelName { get; set; }
        }

        public void InitializeTileButtons()
        {
            TMTTileButton tileButton;
            foreach (Control childControl in this.Controls)
            {
                if (childControl is TMTTileButton)
                {
                    tileButton = childControl as TMTTileButton;
                    //tileButton.SetTheme();
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
                TileButtonClickedEventArgs arg = new TileButtonClickedEventArgs();
                arg.PanelName = (sender as TMTTileButton).PanelName;

                TileButtonClicked(this, arg);
            }
        }
    }
}