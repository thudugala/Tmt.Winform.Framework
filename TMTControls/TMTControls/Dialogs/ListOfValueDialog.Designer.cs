namespace TMT.Controls.WinForms.Dialogs
{
    partial class ListOfValueDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListOfValueDialog));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonOK = new TMT.Controls.WinForms.BaseButton();
            this.DbMain = new TMT.Controls.WinForms.DataGrid.DbDataGridView();
            this.buttonSearch = new TMT.Controls.WinForms.BaseButton();
            this.buttonCancel = new TMT.Controls.WinForms.BaseButton();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonCancel);
            this.panelMain.Controls.Add(this.buttonSearch);
            this.panelMain.Controls.Add(this.DbMain);
            this.panelMain.Controls.Add(this.buttonOK);
            resources.ApplyResources(this.panelMain, "panelMain");
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
            // DbMain
            // 
            this.DbMain.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.DbMain, "DbMain");
            this.DbMain.AutoGenerateColumns = false;
            this.DbMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DbMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DbMain.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DbMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DbMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DbMain.DefaultCellStyle = dataGridViewCellStyle4;
            this.DbMain.DefaultOrderByStatement = null;
            this.DbMain.DefaultWhereStatement = null;
            this.DbMain.EnableHeadersVisualStyles = true;
            this.DbMain.MultiSelect = false;
            this.DbMain.Name = "DbMain";
            this.DbMain.ReadOnly = true;
            this.DbMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DbMain.StandardTab = true;
            this.DbMain.TableName = null;
            this.DbMain.ViewName = null;
            this.DbMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DbMain_CellDoubleClick);
            this.DbMain.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DbMain_DataBindingComplete);
            this.DbMain.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DbMain_RowHeaderMouseDoubleClick);
            this.DbMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DbMain_KeyDown);
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonSearch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
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
            // ListOfValueDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.buttonCancel;
            resources.ApplyResources(this, "$this");
            this.Name = "ListOfValueDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ListOfValueDialog_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DbMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TMT.Controls.WinForms.BaseButton buttonOK;
        private DataGrid.DbDataGridView DbMain;
        private TMT.Controls.WinForms.BaseButton buttonSearch;
        private TMT.Controls.WinForms.BaseButton buttonCancel;
    }
}