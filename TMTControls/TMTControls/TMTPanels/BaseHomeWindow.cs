using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace TMTControls.TMTPanels
{
    [ToolboxItem(false)]
    public partial class BaseHomeWindow : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public event EventHandler<TileButtonClickedEventArgs> TileButtonClicked;

        public BaseHomeWindow()
        {
            InitializeComponent();
        }

        private void TMTPanelHome_Load(object sender, EventArgs e)
        {
            this.InitializeTileButtons();
        }

        private void InitializeTileButtons()
        {
            foreach (var tileButton in this.Controls.OfType<TMTTileButton>())
            {
                if (tileButton.NavigatePanel != null)
                {
                    tileButton.Click += TileButton_Click;
                }
            }
        }

        public void ShowCharts()
        {
            foreach (var chart in this.Controls.OfType<TMTChart>())
            {
                chart.ShowChart();
            }
        }

        private void TileButton_Click(object sender, EventArgs e)
        {
            if (TileButtonClicked != null)
            {
                var myButton = sender as TMTTileButton;

                var arg = new TileButtonClickedEventArgs()
                {
                    AssemblyOfType = this.GetType().Assembly,
                    NavigatePanel = myButton.NavigatePanel
                };
                TileButtonClicked(this, arg);
            }
        }
    }
}