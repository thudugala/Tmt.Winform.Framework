using System;
using System.Collections.Generic;
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
            var tileButtonList = GetButtonsList(this.Controls);
            foreach (var tileButton in tileButtonList)
            {
                if (tileButton.NavigatePanel != null)
                {
                    tileButton.Click += TileButton_Click;
                }
            }
        }

        private List<TileButton> GetButtonsList(ControlCollection controlCollection)
        {
            var controlList = new List<TileButton>();
            foreach (var control in controlCollection)
            {
                if(control is TileButton tileButton)
                {
                    controlList.Add(tileButton);
                }
                else if(control is Panel panel)
                {
                    var childList = GetButtonsList(panel.Controls);
                    controlList.AddRange(childList);
                }
            }

            return controlList;
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