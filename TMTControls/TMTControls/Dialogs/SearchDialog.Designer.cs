namespace TMT.Controls.WinForms.Dialogs
{
    partial class SearchDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDialog));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new TMT.Controls.WinForms.BaseButton();
            this.buttonCancel = new TMT.Controls.WinForms.BaseButton();
            this.checkBoxCaseSensitive = new System.Windows.Forms.CheckBox();
            this.checkBoxTop100 = new System.Windows.Forms.CheckBox();
            this.baseButtonClear = new TMT.Controls.WinForms.BaseButton();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.baseButtonClear);
            this.panelMain.Controls.Add(this.checkBoxTop100);
            this.panelMain.Controls.Add(this.buttonCancel);
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            this.panelMain.Controls.Add(this.buttonOK);
            this.panelMain.Controls.Add(this.checkBoxCaseSensitive);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // tableLayoutPanelMain
            // 
            resources.ApplyResources(this.tableLayoutPanelMain, "tableLayoutPanelMain");
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // checkBoxCaseSensitive
            // 
            resources.ApplyResources(this.checkBoxCaseSensitive, "checkBoxCaseSensitive");
            this.checkBoxCaseSensitive.Name = "checkBoxCaseSensitive";
            this.checkBoxCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // checkBoxTop100
            // 
            resources.ApplyResources(this.checkBoxTop100, "checkBoxTop100");
            this.checkBoxTop100.Checked = true;
            this.checkBoxTop100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTop100.Name = "checkBoxTop100";
            this.checkBoxTop100.UseVisualStyleBackColor = true;
            // 
            // baseButtonClear
            // 
            resources.ApplyResources(this.baseButtonClear, "baseButtonClear");
            this.baseButtonClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.baseButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.baseButtonClear.FlatAppearance.BorderSize = 0;
            this.baseButtonClear.Name = "baseButtonClear";
            this.baseButtonClear.UseVisualStyleBackColor = false;
            this.baseButtonClear.Click += new System.EventHandler(this.BaseButtonClear_Click);
            // 
            // SearchDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonCancel;
            resources.ApplyResources(this, "$this");
            this.Name = "SearchDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SearchDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private TMT.Controls.WinForms.BaseButton buttonOK;
        private TMT.Controls.WinForms.BaseButton buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxCaseSensitive;
        private System.Windows.Forms.CheckBox checkBoxTop100;
        private BaseButton baseButtonClear;
    }
}