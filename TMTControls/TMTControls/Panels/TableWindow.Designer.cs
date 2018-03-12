namespace TMT.Controls.WinForms.Panels
{
    partial class TableWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dbViewTable = new TMT.Controls.WinForms.DataGrid.DbDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dbViewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dbViewTable
            // 
            this.dbViewTable.AllowUserToOrderColumns = true;
            this.dbViewTable.AutoGenerateColumns = false;
            this.dbViewTable.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dbViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dbViewTable.DefaultCellStyle = dataGridViewCellStyle1;
            this.dbViewTable.DefaultOrderByStatement = null;
            this.dbViewTable.DefaultWhereStatement = null;
            this.dbViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbViewTable.EnableHeadersVisualStyles = true;
            this.dbViewTable.Location = new System.Drawing.Point(54, 54);
            this.dbViewTable.Name = "dbViewTable";
            this.dbViewTable.RowTemplate.Height = 24;
            this.dbViewTable.Size = new System.Drawing.Size(746, 524);
            this.dbViewTable.TabIndex = 2;
            this.dbViewTable.TableName = null;
            this.dbViewTable.ViewName = null;
            this.dbViewTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DbTable_CellValueChanged);
            this.dbViewTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DbTable_UserDeletedRow);
            this.dbViewTable.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DbTable_UserDeletingRow);
            // 
            // TableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Controls.Add(this.dbViewTable);
            this.Name = "TableWindow";
            this.Load += new System.EventHandler(this.TableWindow_Load);
            this.Controls.SetChildIndex(this.dbViewTable, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dbViewTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected DataGrid.DbDataGridView dbViewTable;
    }
}
