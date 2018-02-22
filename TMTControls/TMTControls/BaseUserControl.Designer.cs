namespace TMTControls
{
    partial class BaseUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseUserControl));
            this.panelTopPanel = new System.Windows.Forms.Panel();
            this.labelWindowName = new System.Windows.Forms.Label();
            this.buttonNavigateBack = new TMTControls.IconButton();
            this.toolTipBase = new System.Windows.Forms.ToolTip(this.components);
            this.statusStripBase = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelWindowName = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarBase = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelFill = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButtonOptions = new System.Windows.Forms.ToolStripSplitButton();
            this.captureScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTopPanel.SuspendLayout();
            this.statusStripBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopPanel
            // 
            this.panelTopPanel.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.panelTopPanel.Controls.Add(this.labelWindowName);
            this.panelTopPanel.Controls.Add(this.buttonNavigateBack);
            this.panelTopPanel.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.panelTopPanel, "panelTopPanel");
            this.panelTopPanel.Name = "panelTopPanel";
            // 
            // labelWindowName
            // 
            this.labelWindowName.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.labelWindowName, "labelWindowName");
            this.labelWindowName.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.labelWindowName.Name = "labelWindowName";
            // 
            // buttonNavigateBack
            // 
            resources.ApplyResources(this.buttonNavigateBack, "buttonNavigateBack");
            this.buttonNavigateBack.FlatAppearance.BorderSize = 0;
            this.buttonNavigateBack.IconBackColor = System.Drawing.Color.Empty;
            this.buttonNavigateBack.IconBorderColor = System.Drawing.Color.Empty;
            this.buttonNavigateBack.IconForeColor = System.Drawing.Color.White;
            this.buttonNavigateBack.IconLocation = new System.Drawing.Point(0, 0);
            this.buttonNavigateBack.IconType = FontAwesome5.Type.ArrowCircleLeft;
            this.buttonNavigateBack.Name = "buttonNavigateBack";
            this.buttonNavigateBack.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonNavigateBack, resources.GetString("buttonNavigateBack.ToolTip"));
            this.buttonNavigateBack.UseVisualStyleBackColor = true;
            this.buttonNavigateBack.Click += new System.EventHandler(this.ButtonNavigateBack_Click);
            // 
            // statusStripBase
            // 
            this.statusStripBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelWindowName,
            this.progressBarBase,
            this.toolStripStatusLabelFill,
            this.toolStripSplitButtonOptions});
            resources.ApplyResources(this.statusStripBase, "statusStripBase");
            this.statusStripBase.Name = "statusStripBase";
            // 
            // toolStripStatusLabelWindowName
            // 
            this.toolStripStatusLabelWindowName.Name = "toolStripStatusLabelWindowName";
            resources.ApplyResources(this.toolStripStatusLabelWindowName, "toolStripStatusLabelWindowName");
            // 
            // progressBarBase
            // 
            this.progressBarBase.Name = "progressBarBase";
            resources.ApplyResources(this.progressBarBase, "progressBarBase");
            this.progressBarBase.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // toolStripStatusLabelFill
            // 
            this.toolStripStatusLabelFill.Name = "toolStripStatusLabelFill";
            resources.ApplyResources(this.toolStripStatusLabelFill, "toolStripStatusLabelFill");
            this.toolStripStatusLabelFill.Spring = true;
            // 
            // toolStripSplitButtonOptions
            // 
            this.toolStripSplitButtonOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButtonOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureScreenToolStripMenuItem});
            resources.ApplyResources(this.toolStripSplitButtonOptions, "toolStripSplitButtonOptions");
            this.toolStripSplitButtonOptions.Name = "toolStripSplitButtonOptions";
            this.toolStripSplitButtonOptions.ButtonClick += new System.EventHandler(this.ToolStripSplitButtonOptions_ButtonClick);
            // 
            // captureScreenToolStripMenuItem
            // 
            this.captureScreenToolStripMenuItem.Checked = true;
            this.captureScreenToolStripMenuItem.CheckOnClick = true;
            this.captureScreenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.captureScreenToolStripMenuItem.Name = "captureScreenToolStripMenuItem";
            resources.ApplyResources(this.captureScreenToolStripMenuItem, "captureScreenToolStripMenuItem");
            // 
            // BaseUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.statusStripBase);
            this.Controls.Add(this.panelTopPanel);
            this.Name = "BaseUserControl";
            this.Load += new System.EventHandler(this.BaseUserControl_Load);
            this.Validated += new System.EventHandler(this.BaseUserControl_Validated);
            this.panelTopPanel.ResumeLayout(false);
            this.statusStripBase.ResumeLayout(false);
            this.statusStripBase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Label labelWindowName;
        private IconButton buttonNavigateBack;
        protected System.Windows.Forms.ToolTip toolTipBase;
        private System.Windows.Forms.Panel panelTopPanel;
        protected internal System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWindowName;
        protected internal System.Windows.Forms.ToolStripProgressBar progressBarBase;
        private System.Windows.Forms.StatusStrip statusStripBase;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFill;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonOptions;
        private System.Windows.Forms.ToolStripMenuItem captureScreenToolStripMenuItem;
    }
}
