namespace TMT.Controls.WinForms.Dialogs
{
    partial class ColumnManagerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnManagerDialog));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.buttonHideAll = new TMT.Controls.WinForms.BaseButton();
            this.buttonHideOne = new TMT.Controls.WinForms.BaseButton();
            this.buttonShowOne = new TMT.Controls.WinForms.BaseButton();
            this.buttonShowAll = new TMT.Controls.WinForms.BaseButton();
            this.listViewShownColumns = new System.Windows.Forms.ListView();
            this.listViewHiddenColumns = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new TMT.Controls.WinForms.BaseButton();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonOK);
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // tableLayoutPanelMain
            // 
            resources.ApplyResources(this.tableLayoutPanelMain, "tableLayoutPanelMain");
            this.tableLayoutPanelMain.Controls.Add(this.buttonHideAll, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.buttonHideOne, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.buttonShowOne, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.buttonShowAll, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.listViewShownColumns, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.listViewHiddenColumns, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            // 
            // buttonHideAll
            // 
            this.buttonHideAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonHideAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonHideAll.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonHideAll, "buttonHideAll");
            this.buttonHideAll.Name = "buttonHideAll";
            this.buttonHideAll.UseVisualStyleBackColor = false;
            this.buttonHideAll.Click += new System.EventHandler(this.ButtonHideAll_Click);
            // 
            // buttonHideOne
            // 
            this.buttonHideOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonHideOne.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonHideOne.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonHideOne, "buttonHideOne");
            this.buttonHideOne.Name = "buttonHideOne";
            this.buttonHideOne.UseVisualStyleBackColor = false;
            this.buttonHideOne.Click += new System.EventHandler(this.ButtonHideOne_Click);
            // 
            // buttonShowOne
            // 
            this.buttonShowOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonShowOne.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonShowOne.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonShowOne, "buttonShowOne");
            this.buttonShowOne.Name = "buttonShowOne";
            this.buttonShowOne.UseVisualStyleBackColor = false;
            this.buttonShowOne.Click += new System.EventHandler(this.ButtonShowOne_Click);
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonShowAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonShowAll.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonShowAll, "buttonShowAll");
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.UseVisualStyleBackColor = false;
            this.buttonShowAll.Click += new System.EventHandler(this.ButtonShowAll_Click);
            // 
            // listViewShownColumns
            // 
            resources.ApplyResources(this.listViewShownColumns, "listViewShownColumns");
            this.listViewShownColumns.Name = "listViewShownColumns";
            this.tableLayoutPanelMain.SetRowSpan(this.listViewShownColumns, 6);
            this.listViewShownColumns.UseCompatibleStateImageBehavior = false;
            this.listViewShownColumns.View = System.Windows.Forms.View.List;
            // 
            // listViewHiddenColumns
            // 
            resources.ApplyResources(this.listViewHiddenColumns, "listViewHiddenColumns");
            this.listViewHiddenColumns.Name = "listViewHiddenColumns";
            this.tableLayoutPanelMain.SetRowSpan(this.listViewHiddenColumns, 6);
            this.listViewHiddenColumns.UseCompatibleStateImageBehavior = false;
            this.listViewHiddenColumns.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // 
            // ColumnManagerDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Name = "ColumnManagerDialog";
            this.Load += new System.EventHandler(this.ColumnManagerDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private BaseButton buttonHideAll;
        private BaseButton buttonHideOne;
        private BaseButton buttonShowOne;
        private BaseButton buttonShowAll;
        private System.Windows.Forms.ListView listViewShownColumns;
        private System.Windows.Forms.ListView listViewHiddenColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private BaseButton buttonOK;
    }
}