using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace TMTControls.TMTPanels
{
    public partial class TMTPanelHome : UserControl
    {
        [Category("TMT")]
        public event EventHandler<TileButtonClickedEventArgs> TileButtonClicked;

        public TMTPanelHome()
        {
            InitializeComponent();
        }

        private void TMTPanelHome_Load(object sender, EventArgs e)
        {
            this.InitializeTileButtons();
        }

        public void InitializeTileButtons()
        {
            foreach (Control childControl in this.Controls)
            {
                if (childControl is TMTTileButton tileButton)
                {
                    if (string.IsNullOrWhiteSpace(tileButton.PanelName) == false)
                    {
                        tileButton.Click += TileButton_Click;
                    }
                }
            }
        }

        private void TileButton_Click(object sender, EventArgs e)
        {
            if (TileButtonClicked != null)
            {
                TMTTileButton myButton = sender as TMTTileButton;

                var arg = new TileButtonClickedEventArgs()
                {
                    AssemblyOfType = this.GetType().Assembly,
                    PanelFullName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", this.GetType().Namespace, myButton.PanelName)
                };
                TileButtonClicked(this, arg);
            }
        }
    }
}