namespace TMTControls
{
    partial class TMTDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTDialog));
            this.labelHeader = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.labelHeader.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelHeader.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.labelHeader, "labelHeader");
            this.labelHeader.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.labelHeader.Name = "labelHeader";
            // 
            // panelMain
            // 
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            // 
            // TMTDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelHeader);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TMTDialog";
            this.ShowIcon = false;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelHeader;
    }
}