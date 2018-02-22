namespace TMTControls.TMTPanels
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
            this.tmtDataGridViewTable = new TMTControls.TMTDataGrid.TMTDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tmtDataGridViewTable
            // 
            this.tmtDataGridViewTable.AllowUserToOrderColumns = true;
            this.tmtDataGridViewTable.AutoGenerateColumns = false;
            this.tmtDataGridViewTable.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tmtDataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tmtDataGridViewTable.DefaultCellStyle = dataGridViewCellStyle1;
            this.tmtDataGridViewTable.DefaultOrderByStatement = null;
            this.tmtDataGridViewTable.DefaultWhereStatement = null;
            this.tmtDataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tmtDataGridViewTable.EnableHeadersVisualStyles = true;
            this.tmtDataGridViewTable.Location = new System.Drawing.Point(54, 54);
            this.tmtDataGridViewTable.Name = "tmtDataGridViewTable";
            this.tmtDataGridViewTable.RowTemplate.Height = 24;
            this.tmtDataGridViewTable.Size = new System.Drawing.Size(746, 524);
            this.tmtDataGridViewTable.TabIndex = 2;
            this.tmtDataGridViewTable.TableName = null;
            this.tmtDataGridViewTable.ViewName = null;
            this.tmtDataGridViewTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TmtDataGridViewTable_CellValueChanged);
            this.tmtDataGridViewTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.TmtDataGridViewTable_UserDeletedRow);
            this.tmtDataGridViewTable.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.TmtDataGridViewTable_UserDeletingRow);
            // 
            // TableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.Controls.Add(this.tmtDataGridViewTable);
            this.Name = "TableWindow";
            this.Load += new System.EventHandler(this.TableWindow_Load);
            this.Controls.SetChildIndex(this.tmtDataGridViewTable, 0);
            ((System.ComponentModel.ISupportInitialize)(this.tmtDataGridViewTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected TMTDataGrid.TMTDataGridView tmtDataGridViewTable;
    }
}
