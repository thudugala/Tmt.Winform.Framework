namespace TMTControls.TMTPanels
{
    partial class FormWindow
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
            this.components = new System.ComponentModel.Container();
            this.recordSelector = new TMTControls.TMTMultipleColumnComboBox();
            this.errorProviderForm = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSourceForm = new System.Windows.Forms.BindingSource(this.components);
            this.panelFormTop = new System.Windows.Forms.Panel();
            this.labelRecordCount = new System.Windows.Forms.Label();
            this.panelForm = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceForm)).BeginInit();
            this.panelFormTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // recordSelector
            // 
            this.recordSelector.FormattingEnabled = true;
            this.recordSelector.Location = new System.Drawing.Point(10, 15);
            this.recordSelector.Name = "recordSelector";
            this.recordSelector.Size = new System.Drawing.Size(400, 25);
            this.recordSelector.TabIndex = 2;
            this.recordSelector.SelectionChangeCommitted += new System.EventHandler(this.RecordSelector_SelectionChangeCommitted);
            // 
            // errorProviderForm
            // 
            this.errorProviderForm.ContainerControl = this;
            // 
            // bindingSourceForm
            // 
            this.bindingSourceForm.DataSource = typeof(System.Data.DataTable);
            // 
            // panelFormTop
            // 
            this.panelFormTop.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFormTop.Controls.Add(this.labelRecordCount);
            this.panelFormTop.Controls.Add(this.recordSelector);
            this.panelFormTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFormTop.Location = new System.Drawing.Point(54, 54);
            this.panelFormTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelFormTop.Name = "panelFormTop";
            this.panelFormTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelFormTop.Size = new System.Drawing.Size(746, 54);
            this.panelFormTop.TabIndex = 2;
            // 
            // labelRecordCount
            // 
            this.labelRecordCount.AutoSize = true;
            this.labelRecordCount.Location = new System.Drawing.Point(417, 18);
            this.labelRecordCount.Name = "labelRecordCount";
            this.labelRecordCount.Size = new System.Drawing.Size(128, 18);
            this.labelRecordCount.TabIndex = 3;
            this.labelRecordCount.Text = "Record Count is 0";
            // 
            // panelForm
            // 
            this.panelForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForm.Enabled = false;
            this.panelForm.Location = new System.Drawing.Point(54, 108);
            this.panelForm.Margin = new System.Windows.Forms.Padding(0);
            this.panelForm.Name = "panelForm";
            this.panelForm.Padding = new System.Windows.Forms.Padding(10);
            this.panelForm.Size = new System.Drawing.Size(746, 470);
            this.panelForm.TabIndex = 3;
            // 
            // FormWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panelFormTop);
            this.Name = "FormWindow";
            this.Load += new System.EventHandler(this.FormWindow_Load);
            this.Controls.SetChildIndex(this.panelFormTop, 0);
            this.Controls.SetChildIndex(this.panelForm, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceForm)).EndInit();
            this.panelFormTop.ResumeLayout(false);
            this.panelFormTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.ErrorProvider errorProviderForm;
        private System.Windows.Forms.BindingSource bindingSourceForm;
        private System.Windows.Forms.Panel panelFormTop;
        protected System.Windows.Forms.Panel panelForm;
        protected TMTMultipleColumnComboBox recordSelector;
        private System.Windows.Forms.Label labelRecordCount;
    }
}
