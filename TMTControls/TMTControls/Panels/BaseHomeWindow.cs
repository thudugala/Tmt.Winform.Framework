using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace TMT.Controls.WinForms.Panels
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

        private void BaseHomeWindow_Load(object sender, EventArgs e)
        {
            this.InitializeTileButtons();
        }

        private void InitializeTileButtons()
        {
            foreach (var tileButton in this.Controls.OfType<TileButton>())
            {
                if (tileButton.NavigatePanel != null)
                {
                    tileButton.Click += TileButton_Click;
                }
            }
        }

        public void ShowCharts()
        {
            foreach (var chart in this.Controls.OfType<BaseChart>())
            {
                chart.ShowChart();
            }
        }

        private void TileButton_Click(object sender, EventArgs e)
        {
            if (TileButtonClicked != null)
            {
                var myButton = sender as TileButton;

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