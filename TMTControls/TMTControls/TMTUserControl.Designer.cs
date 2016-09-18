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
            this.labelPanelName = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // labelPanelName
            // 
            this.labelPanelName.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.labelPanelName.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelPanelName.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.labelPanelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPanelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPanelName.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.labelPanelName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelPanelName.Location = new System.Drawing.Point(0, 0);
            this.labelPanelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPanelName.Name = "labelPanelName";
            this.labelPanelName.Padding = new System.Windows.Forms.Padding(80, 0, 0, 0);
            this.labelPanelName.Size = new System.Drawing.Size(800, 60);
            this.labelPanelName.TabIndex = 0;
            this.labelPanelName.Text = "Panel Name";
            this.labelPanelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 540);
            this.panelMain.TabIndex = 1;
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = global::TMTControls.Properties.Settings.Default.PanelLabelBackColor;
            this.buttonBack.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::TMTControls.Properties.Settings.Default, "PanelLabelBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.buttonBack.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::TMTControls.Properties.Settings.Default, "PanelLabelForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.ForeColor = global::TMTControls.Properties.Settings.Default.PanelLabelForeColor;
            this.buttonBack.Image = global::TMTControls.Properties.Resources.back;
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 60);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.TabStop = false;
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTipMain.SetToolTip(this.buttonBack, "Go to Home Page");
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // TMTUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelPanelName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TMTUserControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelMain;
        public System.Windows.Forms.Label labelPanelName;
        public System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.ToolTip toolTipMain;
    }
}
