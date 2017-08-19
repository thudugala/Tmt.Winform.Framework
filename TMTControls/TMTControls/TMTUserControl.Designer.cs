namespace TMTControls
{
    partial class TMTUserControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTUserControl));
            this.labelPanelName = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelPanelName
            // 
            this.labelPanelName.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.labelPanelName.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelPanelName.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.labelPanelName, "labelPanelName");
            this.labelPanelName.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.labelPanelName.Name = "labelPanelName";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.buttonBack.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.buttonBack.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.buttonBack.Image = global::TMTControls.Properties.Resources.back;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.TabStop = false;
            this.toolTipMain.SetToolTip(this.buttonBack, resources.GetString("buttonBack.ToolTip"));
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelPanelName);
            this.panelTop.Controls.Add(this.buttonBack);
            resources.ApplyResources(this.panelTop, "panelTop");
            this.panelTop.Name = "panelTop";
            // 
            // TMTUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.Name = "TMTUserControl";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelMain;
        public System.Windows.Forms.Label labelPanelName;
        public System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.ToolTip toolTipMain;
        public System.Windows.Forms.Panel panelTop;
    }
}
