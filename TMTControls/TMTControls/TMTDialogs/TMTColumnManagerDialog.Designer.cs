namespace TMTControls.TMTDialogs
{
    partial class TMTColumnManagerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TMTColumnManagerDialog));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tmtButtonHideAll = new TMTControls.TMTButton();
            this.tmtButtonHideOne = new TMTControls.TMTButton();
            this.tmtButtonShowOne = new TMTControls.TMTButton();
            this.tmtButtonShowAll = new TMTControls.TMTButton();
            this.listViewShownColumns = new System.Windows.Forms.ListView();
            this.listViewHiddenColumns = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tmtButtonOK = new TMTControls.TMTButton();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.Image = global::TMTControls.Properties.Resources.columns;
            resources.ApplyResources(this.labelHeader, "labelHeader");
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tmtButtonOK);
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            resources.ApplyResources(this.panelMain, "panelMain");
            // 
            // tableLayoutPanelMain
            // 
            resources.ApplyResources(this.tableLayoutPanelMain, "tableLayoutPanelMain");
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonHideAll, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonHideOne, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonShowOne, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonShowAll, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.listViewShownColumns, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.listViewHiddenColumns, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            // 
            // tmtButtonHideAll
            // 
            this.tmtButtonHideAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonHideAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonHideAll.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.tmtButtonHideAll, "tmtButtonHideAll");
            this.tmtButtonHideAll.Name = "tmtButtonHideAll";
            this.tmtButtonHideAll.UseVisualStyleBackColor = false;
            this.tmtButtonHideAll.Click += new System.EventHandler(this.tmtButtonHideAll_Click);
            // 
            // tmtButtonHideOne
            // 
            this.tmtButtonHideOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonHideOne.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonHideOne.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.tmtButtonHideOne, "tmtButtonHideOne");
            this.tmtButtonHideOne.Name = "tmtButtonHideOne";
            this.tmtButtonHideOne.UseVisualStyleBackColor = false;
            this.tmtButtonHideOne.Click += new System.EventHandler(this.tmtButtonHideOne_Click);
            // 
            // tmtButtonShowOne
            // 
            this.tmtButtonShowOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonShowOne.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonShowOne.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.tmtButtonShowOne, "tmtButtonShowOne");
            this.tmtButtonShowOne.Name = "tmtButtonShowOne";
            this.tmtButtonShowOne.UseVisualStyleBackColor = false;
            this.tmtButtonShowOne.Click += new System.EventHandler(this.tmtButtonShowOne_Click);
            // 
            // tmtButtonShowAll
            // 
            this.tmtButtonShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonShowAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonShowAll.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.tmtButtonShowAll, "tmtButtonShowAll");
            this.tmtButtonShowAll.Name = "tmtButtonShowAll";
            this.tmtButtonShowAll.UseVisualStyleBackColor = false;
            this.tmtButtonShowAll.Click += new System.EventHandler(this.tmtButtonShowAll_Click);
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
            // tmtButtonOK
            // 
            resources.ApplyResources(this.tmtButtonOK, "tmtButtonOK");
            this.tmtButtonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tmtButtonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonOK.FlatAppearance.BorderSize = 0;
            this.tmtButtonOK.Name = "tmtButtonOK";
            this.tmtButtonOK.UseVisualStyleBackColor = false;
            // 
            // TMTColumnManagerDialog
            // 
            this.AcceptButton = this.tmtButtonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Name = "TMTColumnManagerDialog";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private TMTButton tmtButtonHideAll;
        private TMTButton tmtButtonHideOne;
        private TMTButton tmtButtonShowOne;
        private TMTButton tmtButtonShowAll;
        private System.Windows.Forms.ListView listViewShownColumns;
        private System.Windows.Forms.ListView listViewHiddenColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private TMTButton tmtButtonOK;
    }
}