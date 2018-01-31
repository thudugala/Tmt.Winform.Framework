namespace TMTControls
{
    partial class TMTChart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTChart));
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMain
            // 
            this.chartMain.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartAreaMain";
            this.chartMain.ChartAreas.Add(chartArea2);
            resources.ApplyResources(this.chartMain, "chartMain");
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsTextAutoFit = false;
            legend2.Name = "LegendMain";
            this.chartMain.Legends.Add(legend2);
            this.chartMain.Name = "chartMain";
            this.chartMain.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            // 
            // progressBarMain
            // 
            resources.ApplyResources(this.progressBarMain, "progressBarMain");
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // TMTChart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.progressBarMain);
            this.Controls.Add(this.chartMain);
            this.DoubleBuffered = true;
            this.Name = "TMTChart";
            this.Load += new System.EventHandler(this.TMTChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        public System.Windows.Forms.ProgressBar progressBarMain;

    }
}
