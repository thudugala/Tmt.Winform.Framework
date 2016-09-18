namespace TMTControls
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
            this.labelHeader.Text = "Column Manager";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tmtButtonOK);
            this.panelMain.Controls.Add(this.tableLayoutPanelMain);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonHideAll, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonHideOne, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonShowOne, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.tmtButtonShowAll, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.listViewShownColumns, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.listViewHiddenColumns, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 7;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(515, 332);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tmtButtonHideAll
            // 
            this.tmtButtonHideAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonHideAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonHideAll.FlatAppearance.BorderSize = 0;
            this.tmtButtonHideAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonHideAll.Location = new System.Drawing.Point(237, 107);
            this.tmtButtonHideAll.Name = "tmtButtonHideAll";
            this.tmtButtonHideAll.Size = new System.Drawing.Size(40, 30);
            this.tmtButtonHideAll.TabIndex = 0;
            this.tmtButtonHideAll.Text = ">>";
            this.tmtButtonHideAll.UseVisualStyleBackColor = false;
            this.tmtButtonHideAll.Click += new System.EventHandler(this.tmtButtonHideAll_Click);
            // 
            // tmtButtonHideOne
            // 
            this.tmtButtonHideOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonHideOne.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonHideOne.FlatAppearance.BorderSize = 0;
            this.tmtButtonHideOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonHideOne.Location = new System.Drawing.Point(237, 143);
            this.tmtButtonHideOne.Name = "tmtButtonHideOne";
            this.tmtButtonHideOne.Size = new System.Drawing.Size(40, 30);
            this.tmtButtonHideOne.TabIndex = 1;
            this.tmtButtonHideOne.Text = ">";
            this.tmtButtonHideOne.UseVisualStyleBackColor = false;
            this.tmtButtonHideOne.Click += new System.EventHandler(this.tmtButtonHideOne_Click);
            // 
            // tmtButtonShowOne
            // 
            this.tmtButtonShowOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonShowOne.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonShowOne.FlatAppearance.BorderSize = 0;
            this.tmtButtonShowOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonShowOne.Location = new System.Drawing.Point(237, 179);
            this.tmtButtonShowOne.Name = "tmtButtonShowOne";
            this.tmtButtonShowOne.Size = new System.Drawing.Size(40, 30);
            this.tmtButtonShowOne.TabIndex = 2;
            this.tmtButtonShowOne.Text = "<";
            this.tmtButtonShowOne.UseVisualStyleBackColor = false;
            this.tmtButtonShowOne.Click += new System.EventHandler(this.tmtButtonShowOne_Click);
            // 
            // tmtButtonShowAll
            // 
            this.tmtButtonShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonShowAll.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonShowAll.FlatAppearance.BorderSize = 0;
            this.tmtButtonShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonShowAll.Location = new System.Drawing.Point(237, 215);
            this.tmtButtonShowAll.Name = "tmtButtonShowAll";
            this.tmtButtonShowAll.Size = new System.Drawing.Size(40, 30);
            this.tmtButtonShowAll.TabIndex = 3;
            this.tmtButtonShowAll.Text = "<<";
            this.tmtButtonShowAll.UseVisualStyleBackColor = false;
            this.tmtButtonShowAll.Click += new System.EventHandler(this.tmtButtonShowAll_Click);
            // 
            // listViewShownColumns
            // 
            this.listViewShownColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewShownColumns.Location = new System.Drawing.Point(3, 23);
            this.listViewShownColumns.Name = "listViewShownColumns";
            this.tableLayoutPanelMain.SetRowSpan(this.listViewShownColumns, 6);
            this.listViewShownColumns.Size = new System.Drawing.Size(228, 306);
            this.listViewShownColumns.TabIndex = 4;
            this.listViewShownColumns.UseCompatibleStateImageBehavior = false;
            this.listViewShownColumns.View = System.Windows.Forms.View.List;
            // 
            // listViewHiddenColumns
            // 
            this.listViewHiddenColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHiddenColumns.Location = new System.Drawing.Point(283, 23);
            this.listViewHiddenColumns.Name = "listViewHiddenColumns";
            this.tableLayoutPanelMain.SetRowSpan(this.listViewHiddenColumns, 6);
            this.listViewHiddenColumns.Size = new System.Drawing.Size(229, 306);
            this.listViewHiddenColumns.TabIndex = 5;
            this.listViewHiddenColumns.UseCompatibleStateImageBehavior = false;
            this.listViewHiddenColumns.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Shown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hidden";
            // 
            // tmtButtonOK
            // 
            this.tmtButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tmtButtonOK.AutoSize = true;
            this.tmtButtonOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tmtButtonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tmtButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tmtButtonOK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.tmtButtonOK.FlatAppearance.BorderSize = 0;
            this.tmtButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tmtButtonOK.Location = new System.Drawing.Point(531, 13);
            this.tmtButtonOK.Name = "tmtButtonOK";
            this.tmtButtonOK.Size = new System.Drawing.Size(41, 30);
            this.tmtButtonOK.TabIndex = 1;
            this.tmtButtonOK.Text = "OK";
            this.tmtButtonOK.UseVisualStyleBackColor = false;
            // 
            // TMTColumnManagerDialog
            // 
            this.AcceptButton = this.tmtButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Name = "TMTColumnManagerDialog";
            this.Text = "Column Manager";
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